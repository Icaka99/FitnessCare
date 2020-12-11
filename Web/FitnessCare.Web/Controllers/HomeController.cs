namespace FitnessCare.Web.Controllers
{
    using System.Diagnostics;

    using FitnessCare.Services.Data;
    using FitnessCare.Web.ViewModels;
    using FitnessCare.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IArticleService articlesService;
        private readonly ICategoryService categoryService;
        private readonly IQuotesService quotesService;

        public HomeController(IArticleService articlesService, ICategoryService categoryService, IQuotesService quotesService)
        {
            this.articlesService = articlesService;
            this.categoryService = categoryService;
            this.quotesService = quotesService;
        }

        public IActionResult Index()
        {
            var indexViewModel = new IndexViewModel
            {
                RandomArticles = this.articlesService.GetRandomArticles(4),
                RandomCategories = this.categoryService.GetRandomCategories(4),
                RandomQuote = this.quotesService.GetRandomQuote(),
            };

            return this.View(indexViewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult StatusCodeError(int errorCode)
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
