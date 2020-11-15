namespace FitnessCare.Services.Data
{
    using System.Threading.Tasks;

    public interface IVoteService
    {
        Task VoteAsync(int articleId, bool isUpVote, string userId);

        int GetVotes(int articleId);
    }
}
