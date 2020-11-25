namespace FitnessCare.Services.Data
{
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using FitnessCare.Web.ViewModels.Contacts;

    public class ContactsService : IContactsService
    {
        private readonly ApplicationDbContext db;

        public ContactsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(ContactFormInputModel model, string ip)
        {
            var contactForm = new ContactForm
            {
                Name = model.Name,
                Email = model.Email,
                Title = model.Title,
                Content = model.Content,
                Ip = ip,
            };

            await this.db.ContactForms.AddAsync(contactForm);
            await this.db.SaveChangesAsync();
        }
    }
}
