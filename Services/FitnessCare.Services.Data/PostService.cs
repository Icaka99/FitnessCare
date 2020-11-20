namespace FitnessCare.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using FitnessCare.Web.ViewModels.Forum;

    public class PostService : IPostService
    {
        private readonly ApplicationDbContext db;

        public PostService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(AddPostInputModel input)
        {
            var dbPost = new Post
            {
                Title = input.Title,
                Content = input.Content,
                UserId = input.UserId,
                CategoryId = input.CategoryId,
            };
            await this.db.Posts.AddAsync(dbPost);
            await this.db.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var post = this.db.Posts.Where(x => x.Id == id).FirstOrDefault();
            post.IsDeleted = true;
            this.db.SaveChanges();
        }

        public IEnumerable<PostViewModel> GetAllById(int id)
        {
                return this.db.Posts.Where(x => x.CategoryId == id)
                .Select(x => new PostViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                CommentsCount = x.Comments.Count,
                CreatedOn = x.CreatedOn,
                UserUserName = x.User.UserName,
                Comments = x.Comments,
            }).ToList();
        }

        public int GetCount()
        {
            return this.db.Posts.Count();
        }

        public PostViewModel GetDetails(int id)
        {
            var post = this.db.Posts.Where(x => x.Id == id)
                .Select(x => new PostViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    CreatedOn = x.CreatedOn,
                    UserUserName = x.User.UserName,
                    CommentsCount = x.Comments.Count,
                    Comments = x.Comments,
                })
                .FirstOrDefault();
            return post;
        }
    }
}
