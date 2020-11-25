namespace FitnessCare.Services.Data
{
    using System.Threading.Tasks;

    using FitnessCare.Web.ViewModels.Contacts;

    public interface IContactsService
    {
        Task CreateAsync(ContactFormInputModel model, string ip);
    }
}
