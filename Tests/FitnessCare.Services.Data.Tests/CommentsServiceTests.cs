namespace FitnessCare.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using FitnessCare.Web.ViewModels.Comments;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CommentsServiceTests
    {
        [Fact]
        public async Task CreateMethodShouldAddCorrectNewCommentToDbAndToArticle()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var commentsService = new CommentsService(dbContext);

            var comment = new Comment
            {
                UserUsername = "Icaka99",
                Content = "testContent",
                UserId = "Icaka99",
            };

            var commentList = new List<Comment>();
            commentList.Add(comment);

            var article = new Article
            {
                Id = 1,
                Comments = commentList,
            };

            await dbContext.Articles.AddAsync(article);

            var user = new ApplicationUser
            {
                Id = "Icaka99",
                UserName = "Icaka99",
            };

            await dbContext.Users.AddAsync(user);

            var commentToAdd = new CreateCommentInputModel
            {
                ArticleId = 1,
                PostId = 1,
                Content = "testContent",
                UserId = "Icaka99",
                UserUserName = "Icaka99",
            };

            await commentsService.Create(commentToAdd);

            Assert.NotNull(dbContext.Comments.FirstOrDefaultAsync());
            Assert.Equal("testContent", dbContext.Comments.FirstAsync().Result.Content);
            Assert.Equal("Icaka99", dbContext.Comments.FirstAsync().Result.UserId);
            Assert.Equal("Icaka99", dbContext.Comments.FirstAsync().Result.UserUsername);
        }

        [Fact]
        public async Task IsInArticleMethodShouldReturnTrueIfCommentIsInArticle()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var commentsService = new CommentsService(dbContext);

            var comment = new Comment
            {
                UserUsername = "Icaka99",
                Content = "testContent",
                UserId = "Icaka99",
            };

            var commentList = new List<Comment>();
            commentList.Add(comment);

            var article = new Article
            {
                Id = 1,
                Comments = commentList,
            };

            await dbContext.Articles.AddAsync(article);

            var user = new ApplicationUser
            {
                Id = "Icaka99",
                UserName = "Icaka99",
            };

            await dbContext.Users.AddAsync(user);

            var commentToAdd = new CreateCommentInputModel
            {
                ArticleId = 1,
                PostId = 1,
                Content = "testContent",
                UserId = "Icaka99",
                UserUserName = "Icaka99",
            };

            await commentsService.Create(commentToAdd);

            var result = commentsService.IsInArticle(1, 1);

            Assert.True(result);
        }

        [Fact]
        public async Task IsInArticleMethodShouldReturnFalsIfCommentIsNotInArticle()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var commentsService = new CommentsService(dbContext);

            var comment = new Comment
            {
                UserUsername = "Icaka99",
                Content = "testContent",
                UserId = "Icaka99",
            };

            var commentList = new List<Comment>();
            commentList.Add(comment);

            var article = new Article
            {
                Id = 3,
                Comments = commentList,
            };

            await dbContext.Articles.AddAsync(article);

            var user = new ApplicationUser
            {
                Id = "Icaka99",
                UserName = "Icaka99",
            };

            await dbContext.Users.AddAsync(user);

            var commentToAdd = new CreateCommentInputModel
            {
                ArticleId = 4,
                PostId = 4,
                Content = "testContent",
                UserId = "Icaka99",
                UserUserName = "Icaka99",
            };

            await commentsService.Create(commentToAdd);

            var result = commentsService.IsInArticle(1, 1);

            Assert.False(result);
        }

        [Fact]
        public async Task IsInPostMethodShouldReturnTrueIfCommentIsInPost()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var commentsService = new CommentsService(dbContext);

            var comment = new Comment
            {
                UserUsername = "Icaka99",
                Content = "testContent",
                UserId = "Icaka99",
            };

            var commentList = new List<Comment>();
            commentList.Add(comment);

            var post = new Post
            {
                Id = 1,
                Comments = commentList,
            };

            await dbContext.Posts.AddAsync(post);

            var user = new ApplicationUser
            {
                Id = "Icaka99",
                UserName = "Icaka99",
            };

            await dbContext.Users.AddAsync(user);

            var commentToAdd = new CreateCommentInputModel
            {
                ArticleId = 1,
                PostId = 1,
                Content = "testContent",
                UserId = "Icaka99",
                UserUserName = "Icaka99",
            };

            await commentsService.Create(commentToAdd);

            var result = commentsService.IsInPost(1, 1);

            Assert.True(result);
        }

        [Fact]
        public async Task IsInPostMethodShouldReturnFalsIfCommentIsNotInPost()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var commentsService = new CommentsService(dbContext);

            var comment = new Comment
            {
                UserUsername = "Icaka99",
                Content = "testContent",
                UserId = "Icaka99",
            };

            var commentList = new List<Comment>();
            commentList.Add(comment);

            var post = new Post
            {
                Id = 3,
                Comments = commentList,
            };

            await dbContext.Posts.AddAsync(post);

            var user = new ApplicationUser
            {
                Id = "Icaka99",
                UserName = "Icaka99",
            };

            await dbContext.Users.AddAsync(user);

            var commentToAdd = new CreateCommentInputModel
            {
                ArticleId = 4,
                PostId = 4,
                Content = "testContent",
                UserId = "Icaka99",
                UserUserName = "Icaka99",
            };

            await commentsService.Create(commentToAdd);

            var result = commentsService.IsInPost(1, 1);

            Assert.False(result);
        }
    }
}
