namespace LipitaSystems.Services.Data
{
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.InputModels;
    using Microsoft.EntityFrameworkCore;

    public class ContactMessageService : IContactMessageService
    {
        private readonly IDeletableEntityRepository<ContactMessage> contactMessageRepository;

        public ContactMessageService(IDeletableEntityRepository<ContactMessage> contactMessageRepository)
        {
            this.contactMessageRepository = contactMessageRepository;
        }

        public async Task<int> GetNotDeletedCunt()
        {
            return await this.contactMessageRepository.AllAsNoTracking()
                .CountAsync();
        }

        public async Task SendAsync(ContactFormInputModel input)
        {
            var contactMessage = new ContactMessage
            {
                Name = input.Name,
                Contact = input.Contact,
                Subject = input.Subject,
                Message = input.Message,
            };

            await this.contactMessageRepository.AddAsync(contactMessage);
            await this.contactMessageRepository.SaveChangesAsync();
        }
    }
}
