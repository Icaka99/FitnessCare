namespace FitnessCare.Services.Data
{
    using System.Threading.Tasks;

    using FitnessCare.Web.ViewModels.Home;
    using FitnessCare.Web.ViewModels.Quotes;

    public interface IQuotesService
    {
        QuoteViewModel GetRandomQuote();

        Task CreateAsync(QuoteInputModel model);
    }
}
