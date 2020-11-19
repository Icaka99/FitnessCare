namespace FitnessCare.Services.Data
{
    using System.Collections;
    using System.Collections.Generic;

    using System.Linq;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using FitnessCare.Web.ViewModels.Blog;

    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext db;

        public ArticleService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(AddArticleInputModel input, string id)
        {
            var dbArticle = new Article
            {
                Title = input.Title,
                Content = input.Content,
                UserId = id,
            };
            await this.db.Articles.AddAsync(dbArticle);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<ArticleViewModel> GetAll()
        {
            return this.db.Articles.Select(x => new ArticleViewModel
            {
                Title = x.Title,
                Content = x.Content,
                CreatedOn = x.CreatedOn,
                Id = x.Id,
                UserUserName = x.User.UserName,
                VotesCount = x.Votes.Count,
                Comments = x.Comments,
            }).ToList();
        }

        public IEnumerable<ArticleViewModel> GetArticles(int? take = null, int skip = 0)
        {
            var query = this.db.Articles
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new ArticleViewModel
                {
                    Title = x.Title,
                    Content = x.Content,
                    CreatedOn = x.CreatedOn,
                    Id = x.Id,
                    UserUserName = x.User.UserName,
                    VotesCount = x.Votes.Count,
                    Comments = x.Comments,
                })
                .Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.ToList();
        }

        public IEnumerable<ArticleViewModel> GetAllOrderedArticles()
        {
            return this.db.Articles.Select(x => new ArticleViewModel
            {
                Title = x.Title,
                Content = x.Content,
                CreatedOn = x.CreatedOn,
                Id = x.Id,
                VotesCount = x.Votes.Count,
                Comments = x.Comments,
            })
            .OrderByDescending(x => x.CreatedOn)
            .ToList();
        }

        public int GetCount()
        {
            return this.db.Articles.Count();
        }

        public ArticleViewModel GetDetails(int id)
        {
            var article = this.db.Articles.Where(x => x.Id == id)
                .Select(x => new ArticleViewModel
                {
                    Title = x.Title,
                    Content = x.Content,
                    Id = x.Id,
                    UserUserName = x.User.UserName,
                    CreatedOn = x.CreatedOn,
                    VotesCount = x.Votes.Count,
                    Comments = x.Comments,
                })
                .FirstOrDefault();
            return article;
        }

        public void Delete(int id)
        {
            var article = this.db.Articles.Where(x => x.Id == id).FirstOrDefault();
            article.IsDeleted = true;
            this.db.SaveChanges();
        }
    }
}
