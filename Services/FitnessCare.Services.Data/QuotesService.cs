namespace FitnessCare.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using FitnessCare.Web.ViewModels.Home;
    using FitnessCare.Web.ViewModels.Quotes;

    public class QuotesService : IQuotesService
    {
        private readonly ApplicationDbContext db;

        public QuotesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(QuoteInputModel model)
        {
            var author = this.db.Authors.FirstOrDefault(x => x.Name == model.AuthorName);

            if (author == null)
            {
                author = new Author
                {
                    Name = model.AuthorName,
                };
            }

            var quote = new Quote
            {
                Content = model.Content,
                Author = author,
            };

            await this.db.Quotes.AddAsync(quote);
            await this.db.SaveChangesAsync();
        }

        public QuoteViewModel GetRandomQuote()
        {
            return this.db.Quotes
                .OrderBy(x => Guid.NewGuid())
                .Take(1)
                .Select(x => new QuoteViewModel
                {
                    Content = x.Content,
                    AuthorName = x.Author.Name,
                })
                .FirstOrDefault();
        }
    }
}
