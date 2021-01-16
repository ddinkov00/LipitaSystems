namespace LipitaSystems.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using LipitaSystems.Web.ViewModels.InputModels;

    public interface IContactMessageService
    {
        Task SendAsync(ContactFormInputModel input);
    }
}
