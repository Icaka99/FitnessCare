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

        public IEnumerable<PostViewModel> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public int GetCount()
        {
            return this.db.Posts.Count();
        }

        public PostViewModel GetDetails(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
