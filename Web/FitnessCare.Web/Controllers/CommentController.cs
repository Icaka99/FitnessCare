namespace FitnessCare.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessCare.Data.Models;
    using FitnessCare.Services.Data;
    using FitnessCare.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CommentController : BaseController
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            input.UserId = user.Id;
            input.UserUserName = user.UserName;

            var parentId = input.ParentId == 0 ? (int?)null : input.ParentId;

            if (input.PostId == 0)
            {
                if (parentId.HasValue)
                {
                    if (!this.commentsService.IsInArticle(parentId.Value, input.ArticleId))
                    {
                        return this.BadRequest();
                    }
                }

                if (input.Content == null)
                {
                    return this.RedirectToAction("Article", "Blog", new { id = input.ArticleId });
                }

                await this.commentsService.Create(input, parentId);

                return this.RedirectToAction("Article", "Blog", new { id = input.ArticleId });
            }
            else
            {
                if (parentId.HasValue)
                {
                    if (!this.commentsService.IsInPost(parentId.Value, input.PostId))
                    {
                        return this.BadRequest();
                    }
                }

                if (input.Content == null)
                {
                    return this.RedirectToAction("Post", "Posts", new { id = input.PostId });
                }

                await this.commentsService.Create(input, parentId);

                return this.RedirectToAction("Post", "Posts", new { id = input.PostId });
            }
        }
    }
}
