namespace FitnessCare.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Web.ViewModels.Contacts;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ContactFormsServiceTests
    {
        [Fact]
        public async Task CreateMethodShouldAddCorrectNewContactFormToDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var contactsService = new ContactsService(dbContext);

            var contactFormToAdd = new ContactFormInputModel
            {
                Name = "testName",
                Content = "testContent",
                Email = "testEmail",
                Title = "testTitle",
            };

            await contactsService.CreateAsync(contactFormToAdd, "ip");

            Assert.NotNull(dbContext.ContactForms.FirstOrDefaultAsync());
            Assert.Equal("testName", dbContext.ContactForms.FirstAsync().Result.Name);
            Assert.Equal("testContent", dbContext.ContactForms.FirstAsync().Result.Content);
            Assert.Equal("testEmail", dbContext.ContactForms.FirstAsync().Result.Email);
            Assert.Equal("testTitle", dbContext.ContactForms.FirstAsync().Result.Title);
            Assert.Equal("ip", dbContext.ContactForms.FirstAsync().Result.Ip);
        }
    }
}
