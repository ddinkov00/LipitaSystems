namespace LipitaSystems.Services.Data
{
    using System.Threading.Tasks;

    using LipitaSystems.Data.Common.Repositories;
    using LipitaSystems.Data.Models;
    using LipitaSystems.Services.Data.Contracts;
    using LipitaSystems.Web.ViewModels.InputModels;

    public class ContactMessageService : IContactMessageService
    {
        private readonly IDeletableEntityRepository<ContactMessage> contactMessageRepository;

        public ContactMessageService(IDeletableEntityRepository<ContactMessage> contactMessageRepository)
        {
            this.contactMessageRepository = contactMessageRepository;
        }

        public async Task Send(ContactFormInputModel input)
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
