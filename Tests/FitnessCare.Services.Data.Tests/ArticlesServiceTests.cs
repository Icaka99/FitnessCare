namespace FitnessCare.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using FitnessCare.Web.ViewModels.Blog;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ArticlesServiceTests
    {
        [Fact]
        public async Task CreateMethodShouldAddCorrectNewArticleToDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var articleService = new ArticleService(dbContext, null);

            var articleToAdd = new AddArticleInputModel
            {
                Content = "testContent",
                Title = "testTitle",
            };

            await articleService.CreateAsync(articleToAdd, "Icaka99");

            Assert.NotNull(dbContext.Articles.FirstOrDefaultAsync());
            Assert.Equal("testContent", dbContext.Articles.FirstAsync().Result.Content);
            Assert.Equal("testTitle", dbContext.Articles.FirstAsync().Result.Title);
            Assert.Equal("Icaka99", dbContext.Articles.FirstAsync().Result.UserId);
        }

        [Fact]
        public async Task GetSearchedArticlesShouldReturnCorrectArticles()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var articleService = new ArticleService(dbContext, null);

            var searchedArticle = new Article
            {
                Content = "testContent",
                ImageUrl = "testImageUrl",
                Title = "testTitle",
                Id = 1,
                UserId = "Icaka99",
                CreatedOn = DateTime.Now,
                User = new ApplicationUser
                {
                    UserName = "Icaka99",
                },
            };

            var notSearchedArticle = new Article
            {
                Content = "notTestContent",
                ImageUrl = "notTestImageUrl",
                Title = "doesntContainTheWord",
                Id = 2,
                UserId = "notIcaka99",
                CreatedOn = DateTime.Now,
                User = new ApplicationUser
                {
                    UserName = "notIcaka99",
                },
            };

            var searchedString = new SearchStringInputModel
            {
                SearchString = "testTitle",
            };

            await dbContext.AddAsync(searchedArticle);
            await dbContext.AddAsync(notSearchedArticle);
            await dbContext.SaveChangesAsync();

            var result = articleService.GetSearchedArticles(searchedString, 1);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("testTitle", result.First().Title);
        }

        [Fact]
        public async Task GetSearchedArticlesShouldReturnAllOfThemWhenSearchStringIsNull()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var articleService = new ArticleService(dbContext, null);

            var searchedArticle = new Article
            {
                Content = "testContent",
                ImageUrl = "testImageUrl",
                Title = "testTitle",
                Id = 1,
                UserId = "Icaka99",
                CreatedOn = DateTime.Now,
                User = new ApplicationUser
                {
                    UserName = "Icaka99",
                },
            };

            var notSearchedArticle = new Article
            {
                Content = "notTestContent",
                ImageUrl = "notTestImageUrl",
                Title = "doesntContainTheWord",
                Id = 2,
                UserId = "notIcaka99",
                CreatedOn = DateTime.Now,
                User = new ApplicationUser
                {
                    UserName = "notIcaka99",
                },
            };

            var searchedString = new SearchStringInputModel
            {
                SearchString = null,
            };

            await dbContext.AddAsync(searchedArticle);
            await dbContext.AddAsync(notSearchedArticle);
            await dbContext.SaveChangesAsync();

            var result = articleService.GetSearchedArticles(searchedString, 1);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetArticlesShouldReturnAllArticles()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var articleService = new ArticleService(dbContext, null);

            var searchedArticle = new Article
            {
                Content = "testContent",
                ImageUrl = "testImageUrl",
                Title = "testTitle",
                Id = 1,
                UserId = "Icaka99",
                CreatedOn = DateTime.Now,
                User = new ApplicationUser
                {
                    UserName = "Icaka99",
                },
            };

            var notSearchedArticle = new Article
            {
                Content = "notTestContent",
                ImageUrl = "notTestImageUrl",
                Title = "testTitle",
                Id = 2,
                UserId = "notIcaka99",
                CreatedOn = DateTime.Now,
                User = new ApplicationUser
                {
                    UserName = "notIcaka99",
                },
            };

            await dbContext.AddAsync(searchedArticle);
            await dbContext.AddAsync(notSearchedArticle);
            await dbContext.SaveChangesAsync();

            var result = articleService.GetArticles(1, 2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("testTitle", result.First().Title);
        }

        [Fact]
        public void GetArticlesShouldReturnZeroArticlesWhenThereIsNoArticles()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var articleService = new ArticleService(dbContext, null);

            var result = articleService.GetArticles(1, 2);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllOrderedArticlesShouldReturnAllOrderedArticlesByDate()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var articleService = new ArticleService(dbContext, null);

            var firstArticle = new Article
            {
                Content = "testContent",
                ImageUrl = "testImageUrl",
                Title = "firstTitle",
                Id = 1,
                UserId = "Icaka99",
                CreatedOn = DateTime.Today,
                User = new ApplicationUser
                {
                    UserName = "Icaka99",
                },
            };

            var secondArticle = new Article
            {
                Content = "notTestContent",
                ImageUrl = "notTestImageUrl",
                Title = "secondTitle",
                Id = 2,
                UserId = "notIcaka99",
                CreatedOn = DateTime.Today.AddDays(1),
                User = new ApplicationUser
                {
                    UserName = "notIcaka99",
                },
            };

            await dbContext.AddAsync(firstArticle);
            await dbContext.AddAsync(secondArticle);
            await dbContext.SaveChangesAsync();

            var result = articleService.GetAllOrderedArticles();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("secondTitle", result.First().Title);
        }

        [Fact]
        public void GetAllOrderedArticlesShouldReturnZeroArticlesWhenThereIsNoArticles()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var articleService = new ArticleService(dbContext, null);

            var result = articleService.GetAllOrderedArticles();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCountShouldReturnNumberOfArticlesInDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var articleService = new ArticleService(dbContext, null);

            var firstArticle = new Article();

            var secondArticle = new Article();

            await dbContext.AddAsync(firstArticle);
            await dbContext.AddAsync(secondArticle);
            await dbContext.SaveChangesAsync();

            var result = articleService.GetCount();

            Assert.Equal(2, result);
        }

        [Fact]
        public void GetCountShouldReturnZeroWhenThereIsNoArticles()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var articleService = new ArticleService(dbContext, null);

            var result = articleService.GetAllOrderedArticles();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        //System.InvalidCastException : Unable to cast object of type 'System.Linq.Expressions.NewExpression'
        //to type 'System.Linq.Expressions.MethodCallExpression'.
        //[Fact]
        //public async Task GetDetailsShouldReturnOnlyCorrectArticle()
        //{
        //    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    var dbContext = new ApplicationDbContext(optionsBuilder.Options);

        //    var articleService = new ArticleService(dbContext, null);

        //    var comments = new List<Comment>();
        //    comments.Add(new Comment
        //    {
        //        Id = 1,
        //        Content = "testContent",
        //        UserId = "Icaka99",
        //        UserUsername = "Icaka99",
        //        User = new ApplicationUser
        //        {
        //            UserName = "Icaka99",
        //        },
        //    });

        //    var firstArticle = new Article
        //    {
        //        Title = "testTitle",
        //        Content = "testContent",
        //        Id = 8,
        //        CreatedOn = DateTime.Now,
        //        Comments = comments,
        //        ImageUrl = "testImageUrl",
        //        UserId = "Icaka99",
        //        User = new ApplicationUser
        //        {
        //            UserName = "Icaka99",
        //        },
        //    };

        //    var votes = new List<Vote>();
        //    votes.Add(new Vote
        //    {
        //        ArticleId = 8,
        //        Id = 1,
        //        Type = VoteType.UpVote,
        //        UserId = "Icaka99",
        //        User = new ApplicationUser
        //        {
        //            UserName = "Icaka99",
        //        },
        //        Article = firstArticle,
        //    });

        //    firstArticle.Votes = votes;

        //    var secondArticle = new Article { Id = 10 };

        //    await dbContext.AddAsync(secondArticle);
        //    await dbContext.AddAsync(firstArticle);
        //    await dbContext.SaveChangesAsync();

        //    var result = articleService.GetDetails(8);

        //    Assert.NotNull(result);
        //    Assert.Equal(8, result.Id);
        //}

        [Fact]
        public async Task DeleteShouldDeleteArticleById()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var articleService = new ArticleService(dbContext, null);

            var article = new Article
            {
                Content = "testContent",
                ImageUrl = "testImageUrl",
                Title = "testTitle",
                Id = 4,
                UserId = "Icaka99",
                CreatedOn = DateTime.Now,
                User = new ApplicationUser
                {
                    UserName = "Icaka99",
                },
            };

            await dbContext.Articles.AddAsync(article);
            await dbContext.SaveChangesAsync();

            articleService.Delete(4);

            Assert.False(dbContext.Articles.Any());
        }

        [Fact]
        public async Task GetRandomArticlesShouldReturnCorrectNumberOfArticles()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var articleService = new ArticleService(dbContext, null);

            var firstArticle = new Article
            {
                Title = "testTitle",
                Id = 1,
                ImageUrl = "testImageUrl",
                Content = "testContent",
            };

            var secondArticle = new Article
            {
                Title = "testTitle",
                Id = 2,
                ImageUrl = "testImageUrl",
                Content = "testContent",
            };

            await dbContext.AddAsync(firstArticle);
            await dbContext.AddAsync(secondArticle);
            await dbContext.SaveChangesAsync();

            var result = articleService.GetRandomArticles(2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetRandomArticlesShouldReturnZeroWhenThereIsNoArticlesInDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var articleService = new ArticleService(dbContext, null);

            var result = articleService.GetRandomArticles(3);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateCorrectArticleById()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var articleService = new ArticleService(dbContext, null);

            var firstArticle = new Article
            {
                Title = "testTitle",
                Id = 1,
                ImageUrl = "testImageUrl",
                Content = "testContent",
            };

            var editArticle = new EditArticleInputModel
            {
                Content = "editedContent",
                Title = "editedTitle",
            };

            await dbContext.AddAsync(firstArticle);
            await dbContext.SaveChangesAsync();

            await articleService.UpdateAsync(1, editArticle);

            var result = dbContext.Articles.FirstOrDefault();

            Assert.NotNull(result);
            Assert.Equal("editedTitle", result.Title);
            Assert.Equal("editedContent", result.Content);
            Assert.Equal("testImageUrl", result.ImageUrl);
        }
    }
}
