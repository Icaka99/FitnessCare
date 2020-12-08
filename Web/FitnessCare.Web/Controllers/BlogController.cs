namespace FitnessCare.Web.Controllers
{
    using System.Linq;

    using FitnessCare.Services.Data;
    using FitnessCare.Web.ViewModels.Blog;
    using Microsoft.AspNetCore.Mvc;

    public class BlogController : BaseController
    {
        private const int ItemsPerPage = 5;

        private readonly IArticleService articleService;

        public BlogController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public IActionResult Blog(int id)
        {
            if (id == 0)
            {
                id = 1;
            }

            var articles = this.articleService.GetArticles(id, ItemsPerPage);
            var orderedArticles = this.articleService.GetAllOrderedArticles();

            var model = new ArticlesListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                ArticlesCount = this.articleService.GetCount(),
                Articles = articles,
                OrderedArticles = orderedArticles,
                PageNumber = id,
                SearchString = null,
            };

            return this.View(model);
        }

        public IActionResult List(SearchStringInputModel input, int id)
        {
            if (id == 0)
            {
                id = 1;
            }

            var articles = this.articleService.GetSearchedArticles(input, id, ItemsPerPage);
            var orderedArticles = this.articleService.GetAllOrderedArticles();

            var model = new ArticlesListViewModel
            {
                Articles = articles,
                ArticlesCount = articles.Count(),
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                OrderedArticles = orderedArticles,
                SearchString = input.SearchString,
            };

            return this.View(nameof(this.Blog), model);
        }
    }
}
