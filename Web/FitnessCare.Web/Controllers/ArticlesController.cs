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

        public ArticlesController(IArticleService articleService, UserManager<ApplicationUser> userManager)
        {
            this.articleService = articleService;
            this.userManager = userManager;
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
            return this.Redirect("/Blog/ThankYou");
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }

        public IActionResult Article(int id)
        {
            var article = this.articleService.GetDetails(id);
            if (article == null)
            {
                return this.NotFound();
            }

            return this.View(article);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            this.articleService.Delete(id);
            return this.Redirect("/Articles/DeleteSuccess");
        }

        public IActionResult DeleteSuccess()
        {
            return this.View();
        }
    }
}
