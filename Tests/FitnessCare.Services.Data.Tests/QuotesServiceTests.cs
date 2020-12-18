namespace FitnessCare.Services.Data.Tests
{
    using System;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class QuotesServiceTests
    {
        [Fact]
        public void GetAllShouldReturnAllCategories()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var quotesService = new QuotesService(dbContext);

            var firstQuote = new Quote
            {
                Id = 1,
                Content = "testContent",
            };

            var secondQuote = new Quote
            {
                Id = 2,
                Content = "testContent",
            };

            dbContext.Quotes.Add(firstQuote);
            dbContext.Quotes.Add(secondQuote);
            dbContext.SaveChangesAsync();

            var result = quotesService.GetRandomQuote();

            Assert.NotNull(result);
            Assert.Equal("testContent", result.Content);
        }
    }
}
