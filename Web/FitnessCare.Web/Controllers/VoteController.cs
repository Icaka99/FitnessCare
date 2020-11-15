namespace FitnessCare.Web.Controllers
{
    using System.Threading.Tasks;

    using FitnessCare.Data.Models;
    using FitnessCare.Services.Data;
    using FitnessCare.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService voteService;
        private readonly UserManager<ApplicationUser> userManager;

        public VoteController(IVoteService voteService, UserManager<ApplicationUser> userManager)
        {
            this.voteService = voteService;
            this.userManager = userManager;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        [Consumes("application/json")]
        public async Task<ActionResult<int>> Post(int articleId, bool isUpVote)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.voteService.VoteAsync(articleId, isUpVote, user.Id);

            var votes = this.voteService.GetVotes(articleId);

            return votes;
        }
    }
}
