namespace FitnessCare.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessCare.Services.Data;
    using FitnessCare.Web.ViewModels.Forum;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ForumController : BaseController
    {
        private readonly ICategoryService categoryService;

        public ForumController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Forum()
        {
            var categories = this.categoryService.GetAll();

            return this.View(categories);
        }

        [Authorize]
        public IActionResult AddCategory()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCategory(AddCategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.categoryService.CreateAsync(input);

            return this.Redirect("/Forum/Forum");
        }
    }
}
