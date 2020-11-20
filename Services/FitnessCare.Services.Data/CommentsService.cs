namespace FitnessCare.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using FitnessCare.Web.ViewModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext db;

        public CommentsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task Create(CreateCommentInputModel input, int? parentId = null)
        {
            var user = this.db.Users.Find(input.UserId);

            var comment = new Comment
            {
                Content = input.Content,
                ParentId = parentId,
                UserId = input.UserId,
                UserUsername = input.UserUserName,
            };

            await this.db.Comments.AddAsync(comment);
            this.db.Users.FirstOrDefault(x => x.Id == input.UserId).Comments.Add(comment);
            if (input.ArticleId == 0)
            {
                this.db.Posts.FirstOrDefault(x => x.Id == input.PostId).Comments.Add(comment);
            }
            else
            {
                this.db.Articles.FirstOrDefault(x => x.Id == input.ArticleId).Comments.Add(comment);
            }

            await this.db.SaveChangesAsync();
        }

        public bool IsInArticle(int commentId, int articleId)
        {
            var commentArticleId = this.db.Articles
                .Where(x => x.Comments.FirstOrDefault(x => x.Id == commentId).Id == commentId)
                .Select(x => x.Id)
                .FirstOrDefault();

            return commentArticleId == articleId;
        }

        public bool IsInPost(int commentId, int postId)
        {
            var commentPostId = this.db.Posts
                .Where(x => x.Comments.FirstOrDefault(x => x.Id == commentId).Id == commentId)
                .Select(x => x.Id)
                .FirstOrDefault();

            return commentPostId == postId;
        }
    }
}
