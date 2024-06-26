﻿using Task1.Model;
using Task1.Model.Domain;
namespace Task1.Repositories.Interface
{
    public interface IEmailAddressRepository
    {
        Task<EmailAddress> CreateAsync(EmailAddress email);
        Task<List<EmailAddress>> GetAllAsync();
        // Task<EmailAddress> GetByIdAsync(Guid id);

        public List<Dictionary<string, string>> SeparateString(string inputString);

        Task<List<EmailAddress>> DeleteEmail(Guid id);
        Task<EmailAddress> GetById(Guid id);
        Task<EmailAddress> EditByID(Guid Id,EmailAddress emailAddress);
        public Task<List<StoreProceedure>> GetAllAsyncSP();
    }
}
