using Microsoft.EntityFrameworkCore;
using Task1.Model.Domain;  
namespace Task1.Data
{
    public class ApplicationDbContext: DbContext
    {
        //Create a constructer 
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        //Create properties
        //userName comes from Domain model 
        //DBset is collection of entities in a relational Databse, 
        public DbSet<EmailAddress> emailAddress { get; set; }
    }
}