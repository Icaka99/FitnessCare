namespace FitnessCare.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessCare.Common;
    using FitnessCare.Services.Data;
    using FitnessCare.Services.Messaging;
    using FitnessCare.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : BaseController
    {
        private readonly IContactsService contactsService;
        private readonly IEmailSender emailSender;

        public ContactsController(IContactsService contactsService, IEmailSender emailSender)
        {
            this.contactsService = contactsService;
            this.emailSender = emailSender;
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactFormInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // TODO: Extract to IP provider (service)
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();

            await this.contactsService.CreateAsync(model, ip);

            await this.SendEmail(model);

            this.TempData["Message"] = "Successfully sent E-mail. Thank You!";

            return this.Redirect("/Home/Index");
        }

        private async Task SendEmail(ContactFormInputModel model)
        {
            await this.emailSender.SendEmailAsync(
                            model.Email,
                            model.Name,
                            GlobalConstants.SystemEmail,
                            model.Title,
                            model.Content);
        }
    }
}
