﻿namespace FitnessCare.Web.Controllers
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
        public async Task<ActionResult<int>> Post(VoteInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.voteService.VoteAsync(input.ArticleId, input.IsUpVote, user.Id);

            var votes = this.voteService.GetVotes(input.ArticleId);

            return votes;
        }
    }
}
