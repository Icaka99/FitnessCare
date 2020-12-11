namespace FitnessCare.Services.Data
{
    using System;
    using System.Linq;

    using FitnessCare.Data;
    using FitnessCare.Web.ViewModels.Home;

    public class QuotesService : IQuotesService
    {
        private readonly ApplicationDbContext db;

        public QuotesService(ApplicationDbContext db)
        {
            this.db = db;
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
