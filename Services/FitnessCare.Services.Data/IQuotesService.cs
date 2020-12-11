namespace FitnessCare.Services.Data
{
    using FitnessCare.Web.ViewModels.Home;

    public interface IQuotesService
    {
        QuoteViewModel GetRandomQuote();
    }
}
