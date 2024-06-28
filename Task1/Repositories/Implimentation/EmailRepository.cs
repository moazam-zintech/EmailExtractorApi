using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Task1.Data;
using Task1.Model;
using Task1.Model.Domain;
using Task1.Repositories.Interface;
namespace Task1.Model
{
    public class StoreProceedure
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid ID { get; set; }
    }
}
namespace Task1.Repositories.Implimentation
{
    public class EmailAddressRepository : IEmailAddressRepository
    {
        //We can use this private fild inside the contoller
        private readonly ApplicationDbContext dbContext;
        public EmailAddressRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            //inject DB class which we injected in program.cs
        }

        public async Task<EmailAddress> EditByID(Guid Id, EmailAddress emailAddress)
        {

            var contact = dbContext.emailAddress.Find(Id);
            if (contact != null)
            {
                contact.FirstName = emailAddress.FirstName;
                contact.LastName = emailAddress.LastName;
                contact.Email = emailAddress.Email;
                dbContext.SaveChanges();
            }
            return contact;
        }
        public async Task<EmailAddress> GetById(Guid id)
        {
            var contact = dbContext.emailAddress.Find(id);

            return contact;
        }
        public async Task<List<EmailAddress>> DeleteEmail(Guid id)
        {
            var contact = dbContext.emailAddress.Find(id);
            dbContext.Remove(contact);
            dbContext.SaveChanges();

            return await dbContext.emailAddress.ToListAsync();
        }


        public async Task<EmailAddress> CreateAsync(EmailAddress email)
        {
            //Now we use injected servises
            await dbContext.emailAddress.AddAsync(email);
            await dbContext.SaveChangesAsync();
            return email;
        }
        public async Task<List<EmailAddress>> GetAllAsync()
        {
            return await dbContext.emailAddress.ToListAsync();
        }
        public async Task<List<StoreProceedure>> GetAllAsyncSP()
        {
            return await dbContext.storePro.FromSqlRaw("Test").ToListAsync();
        }
        public List<Dictionary<string, string>> SeparateString(string inputString)
        {
            List<Dictionary<string, string>> separatedLists = new List<Dictionary<string, string>>();
            string[] entries = inputString.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string entry in entries)
            {
                string[] parts = entry.Split();
                if (parts.Length < 2)
                {
                    Console.WriteLine($"Error: Invalid entry '{entry}'. It should contain at least a first name and an email.");
                    continue;
                }
                string firstName = parts[0];
                string lastName = string.Join(" ", parts, 1, parts.Length - 2);
                string email = parts[parts.Length - 1];

                if (IsValidEmail(email))
                {
                    Dictionary<string, string> entryDict = new Dictionary<string, string>();
                    entryDict.Add("firstName", firstName);
                    entryDict.Add("lastName", lastName);
                    entryDict.Add("email", email);
                    separatedLists.Add(entryDict);
                }
                else
                {
                    Dictionary<string, string> entryDict = new Dictionary<string, string>();
                    entryDict.Add("incorrectEmail", email);
                    /*           entryDict.Add("incorrectFname", firstName);
                               entryDict.Add("incorrectLname", lastName);*/
                    // separatedLists.Add(entryDict);
                    //Console.WriteLine($"Error: Invalid email '{email}' for entry      '{entry}'.");
                }
            }
            return separatedLists;
        }
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }


        /*     public async Task<EmailAddress> GetById(Guid id)
             {
                 var param = new SqlParameter("@ProductID", id);
                 var emailsDetails = await Task.Run( => dbContext.emailAddress.FromSqlRaw(@"exec GetProductbyId @ProductID", param).ToListAsync);
                 return emailsDetails;
             }*/
    }
}