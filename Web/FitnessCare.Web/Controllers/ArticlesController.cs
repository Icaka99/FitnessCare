namespace FitnessCare.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessCare.Data.Models;
    using FitnessCare.Services.Data;
    using FitnessCare.Web.ViewModels.Blog;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ArticlesController : BaseController
    {
        private readonly IArticleService articleService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoryService categoryService;

        public ArticlesController(IArticleService articleService, UserManager<ApplicationUser> userManager, ICategoryService categoryService)
        {
            this.articleService = articleService;
            this.userManager = userManager;
            this.categoryService = categoryService;
        }

        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddArticleInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.articleService.CreateAsync(input, user.Id);

            this.TempData["Message"] = "Article added successfully.";
            return this.Redirect("/Blog/Blog");
        }

        public IActionResult Article(int id)
        {
            var article = this.articleService.GetDetails(id);
            if (article == null)
            {
                return this.NotFound();
            }

            article.Categories = this.categoryService.GetRandomCategories(6);

            return this.View(article);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            this.articleService.Delete(id);

            this.TempData["Message"] = "Article is deleted successfully!";
            return this.Redirect("/Blog/Blog");
        }

        public IActionResult Edit(int id)
        {
            var article = this.articleService.GetDetails(id);

            var model = new EditArticleInputModel
            {
                Id = article.Id,
                Content = article.Content,
                Title = article.Title,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditArticleInputModel article)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.articleService.UpdateAsync(id, article);

            return this.RedirectToAction(nameof(this.Article), new { id });
        }
    }
}
