namespace FitnessCare.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;

    public class VoteService : IVoteService
    {
        private readonly ApplicationDbContext db;

        public VoteService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int GetVotes(int articleId)
        {
            var votes = this.db.Votes.Where(x => x.ArticleId == articleId).Sum(x => (int)x.Type);

            return votes;
        }

        public async Task VoteAsync(int articleId, bool isUpVote, string userId)
        {
            var vote = this.db.Votes.FirstOrDefault(x => x.ArticleId == articleId && x.UserId == userId);

            if (vote != null)
            {
                vote.Type = isUpVote ? VoteType.UpVote : VoteType.DownVote;
            }
            else
            {
                vote = new Vote
                {
                    ArticleId = articleId,
                    UserId = userId,
                    Type = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                };

                await this.db.Votes.AddAsync(vote);
            }

            await this.db.SaveChangesAsync();
        }
    }
}
