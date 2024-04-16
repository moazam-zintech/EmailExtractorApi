using Microsoft.EntityFrameworkCore;
using Task1.Data;
using Task1.Model.Domain;
using Task1.Repositories.Interface;

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
    }
}