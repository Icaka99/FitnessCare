namespace FitnessCare.Web.Controllers
{
    using System.Security.Claims;

    using FitnessCare.Services.Data;
    using FitnessCare.Web.ViewModels.Blog;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class BlogController : BaseController
    {
        private readonly IArticleService articleService;

        public BlogController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public IActionResult Blog()
        {
            var articles = this.articleService.GetAll();
            var orderedArticles = this.articleService.GetAllOrderedArticles();

            var model = new ArticlesListViewModel
            {
                Articles = articles,
                OrderedArticles = orderedArticles,
            };
            return this.View(model);
        }

        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddArticleInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.articleService.Create(input, userId);
            return this.Redirect("/Blog/ThankYou");
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }

        public IActionResult Article(int id)
        {
            var article = this.articleService.GetDetails(id);
            return this.View(article);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            this.articleService.Delete(id);
            return this.Redirect("/Blog/DeleteSuccess");
        }

        public IActionResult DeleteSuccess()
        {
            return this.View();
        }
    }
}
