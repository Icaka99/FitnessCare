namespace FitnessCare.Services.Data
{
    using System;
    using System.Collections.Generic;

    using System.Linq;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using FitnessCare.Web.ViewModels.Blog;

    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext db;
        private readonly ICloudinaryService cloudinaryService;
        private readonly string defaultArticlePicture = "https://res.cloudinary.com/icaka99/image/upload/v1607075451/article_n65evk.jpg";

        public ArticleService(ApplicationDbContext db, ICloudinaryService cloudinaryService)
        {
            this.db = db;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task CreateAsync(AddArticleInputModel input, string id)
        {
            string imageUrl = this.defaultArticlePicture;

            if (input.PictureFile != null)
            {
                imageUrl = await this.cloudinaryService.UploudAsync(input.PictureFile);
            }

            var dbArticle = new Article
            {
                Title = input.Title,
                Content = input.Content,
                UserId = id,
                ImageUrl = imageUrl,
            };
            await this.db.Articles.AddAsync(dbArticle);
            await this.db.SaveChangesAsync();
        }

        public IEnumerable<ArticleViewModel> GetSearchedArticles(SearchStringInputModel input, int page, int itemsPerPage = 1)
        {
            if (input.SearchString == null)
            {
                input.SearchString = string.Empty;
            }

            return this.db.Articles
                .Where(x => x.Title.ToLower().Contains(input.SearchString.ToLower()))
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new ArticleViewModel
                {
                    Title = x.Title,
                    Content = x.Content,
                    CreatedOn = x.CreatedOn,
                    Id = x.Id,
                    UserUserName = x.User.UserName,
                    ImageUrl = x.ImageUrl,
                }).ToList();
        }

        public IEnumerable<ArticleViewModel> GetArticles(int page, int itemsPerPage = 1)
        {
            var articles = this.db.Articles
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .Select(x => new ArticleViewModel
                {
                    Title = x.Title,
                    Content = x.Content,
                    CreatedOn = x.CreatedOn,
                    Id = x.Id,
                    UserUserName = x.User.UserName,
                    ImageUrl = x.ImageUrl,
                }).ToList();

            return articles.ToList();
        }

        public IEnumerable<ArticleViewModel> GetAllOrderedArticles()
        {
            return this.db.Articles.Select(x => new ArticleViewModel
            {
                Title = x.Title,
                CreatedOn = x.CreatedOn,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
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
                    VotesCount = this.db.Votes.Where(x => x.ArticleId == id).Sum(x => (int)x.Type),
                    Comments = x.Comments,
                    ImageUrl = x.ImageUrl,
                })
                .FirstOrDefault();

            foreach (var comment in article.Comments)
            {
                if (comment.User == null)
                {
                    comment.User = this.db.Users.Where(x => x.Id == comment.UserId).FirstOrDefault();
                }
            }

            return article;
        }

        public void Delete(int id)
        {
            var article = this.db.Articles.Where(x => x.Id == id).FirstOrDefault();
            article.IsDeleted = true;
            this.db.SaveChanges();
        }

        public IEnumerable<ArticleViewModel> GetRandomArticles(int count)
        {
            return this.db.Articles
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .Select(x => new ArticleViewModel
                {
                    Title = x.Title,
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Content = x.Content,
                    CreatedOn = x.CreatedOn,
                })
                .ToList();
        }

        public async Task UpdateAsync(int id, EditArticleInputModel input)
        {
            var article = this.db.Articles.FirstOrDefault(x => x.Id == id);
            article.Content = input.Content;
            article.Title = input.Title;

            string imageUrl = article.ImageUrl;

            if (input.PictureFile != null)
            {
                imageUrl = await this.cloudinaryService.UploudAsync(input.PictureFile);
            }

            article.ImageUrl = imageUrl;

            await this.db.SaveChangesAsync();
        }
    }
}
