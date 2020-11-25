namespace FitnessCare.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using FitnessCare.Data.Models;
    using FitnessCare.Services.Data;
    using FitnessCare.Web.ViewModels.Blog;
    using Microsoft.AspNetCore.Identity;
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

            var articles = this.articleService.GetArticles(id, 5);
            var orderedArticles = this.articleService.GetAllOrderedArticles();

            var model = new ArticlesListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                ArticlesCount = this.articleService.GetCount(),
                Articles = articles,
                OrderedArticles = orderedArticles,
                PageNumber = id,
            };

            return this.View(model);
        }
    }
}
