namespace FitnessCare.Services.Data
{
    using System.Threading.Tasks;

    using FitnessCare.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task Create(CreateCommentInputModel input, int? parentId = null);

        bool IsInArticle(int commentId, int articleId);

        bool IsInPost(int commentId, int postId);
    }
}
