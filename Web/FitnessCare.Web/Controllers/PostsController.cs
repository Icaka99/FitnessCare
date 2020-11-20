namespace FitnessCare.Web.Controllers
{
    using FitnessCare.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : BaseController
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        public IActionResult Post(int id)
        {
            var post = this.postService.GetDetails(id);

            return this.View(post);
        }
    }
}
