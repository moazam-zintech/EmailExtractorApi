using Task1.Model.Domain;

namespace Task1.Repositories.Interface
{
    public interface IEmailAddressRepository
    {
        Task<EmailAddress> CreateAsync(EmailAddress email);
        Task<List<EmailAddress>> GetAllAsync();
    }
}
