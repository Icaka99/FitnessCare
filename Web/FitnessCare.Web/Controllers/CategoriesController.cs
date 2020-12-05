namespace FitnessCare.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessCare.Data.Models;
    using FitnessCare.Services.Data;
    using FitnessCare.Web.ViewModels.Forum;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        private readonly IPostService postService;
        private readonly UserManager<ApplicationUser> userManager;

        public CategoriesController(IPostService postService, UserManager<ApplicationUser> userManager)
        {
            this.postService = postService;
            this.userManager = userManager;
        }

        public IActionResult Category(int id)
        {
            var categories = this.postService.GetAllById(id);

            return this.View(categories);
        }

        [Authorize]
        public IActionResult AddPost()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPost(AddPostInputModel input, int value)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            input.CategoryId = value;
            input.UserId = user.Id;

            await this.postService.CreateAsync(input);

            this.TempData["Message"] = "Post added successfully";

            return this.Redirect($"/Categories/Category?Id={input.CategoryId}");
        }
    }
}
