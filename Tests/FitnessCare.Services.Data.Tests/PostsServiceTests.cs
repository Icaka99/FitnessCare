namespace FitnessCare.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using FitnessCare.Web.ViewModels.Forum;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class PostsServiceTests
    {
        [Fact]
        public async Task CreateMethodShouldAddCorrectNewPostToDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var postService = new PostService(dbContext);

            var postToAdd = new AddPostInputModel
            {
                CategoryId = 1,
                Content = "testContent",
                Title = "testTitle",
                UserId = "testUser",
            };

            await postService.CreateAsync(postToAdd);

            Assert.NotNull(dbContext.Posts.FirstOrDefaultAsync());
            Assert.Equal("testContent", dbContext.Posts.FirstAsync().Result.Content);
            Assert.Equal("testTitle", dbContext.Posts.FirstAsync().Result.Title);
            Assert.Equal(1, dbContext.Posts.FirstAsync().Result.CategoryId);
        }

        [Fact]
        public async Task DeleteMethodShouldDeletePostById()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var postService = new PostService(dbContext);

            var post = new Post
            {
                Id = 5,
                Title = "testTitle",
                Content = "testContent",
                CategoryId = 1,
                UserId = "testUser",
            };

            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();

            postService.Delete(5);

            Assert.False(dbContext.Categories.Any());
        }

        [Fact]
        public async Task GetAllShouldReturnAllPostsByCategoryId()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var postService = new PostService(dbContext);

            var firstPost = new Post
            {
                Id = 4,
                Title = "testTitle",
                Content = "testContent",
                CategoryId = 2,
                UserId = "testUser",
            };

            var secondPost = new Post
            {
                Id = 5,
                Title = "testTitle",
                Content = "testContent",
                CategoryId = 2,
                UserId = "testUser",
            };

            await dbContext.Posts.AddAsync(firstPost);
            await dbContext.Posts.AddAsync(secondPost);
            await dbContext.SaveChangesAsync();

            var result = postService.GetAllById(2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("testTitle", result.FirstOrDefault().Title);
            Assert.Equal("testContent", result.FirstOrDefault().Content);
            Assert.Equal(2, result.FirstOrDefault().CategoryId);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectCountOfPostsInDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var postService = new PostService(dbContext);

            var firstPost = new Post();

            var secondPost = new Post();

            await dbContext.Posts.AddAsync(firstPost);
            await dbContext.Posts.AddAsync(secondPost);
            await dbContext.SaveChangesAsync();

            var result = postService.GetCount();

            Assert.Equal(2, result);
        }

        // [Fact]
        // public async Task GetDetailsShouldReturnCorrectDetailsAboutPostById()
        // {
        //    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseInMemoryDatabase(Guid.NewGuid().ToString());
        //    var dbContext = new ApplicationDbContext(optionsBuilder.Options);

        // var postService = new PostService(dbContext);

        // var user = new ApplicationUser { Id = "Icaka99", UserName = "Icaka99", };

        // var comments = new List<Comment>
        //    {
        //        new Comment
        //        {
        //            Content = "content",
        //            Id = 1,
        //            User = user,
        //            UserId = user.Id,
        //            UserUsername = user.UserName,
        //        },
        //    };

        // dbContext.Comments.AddRange(comments);

        // await dbContext.Users.AddAsync(user);

        // var firstPost = new Post
        //    {
        //        Id = 5,
        //        Content = "testContent",
        //        Title = "testTitle",
        //        CategoryId = 2,
        //        Comments = comments,
        //        User = user,
        //    };

        // var secondPost = new Post
        //    {
        //        Id = 4,
        //        Content = "testContent1",
        //        Title = "testTitle1",
        //        CategoryId = 2,
        //        Comments = comments,
        //        User = user,
        //    };

        // await dbContext.Posts.AddAsync(firstPost);
        //    await dbContext.Posts.AddAsync(secondPost);
        //    await dbContext.SaveChangesAsync();

        // var result = postService.GetDetails(5);

        // Assert.NotNull(result);
        //    Assert.Equal("testTitle", result.Title);
        //    Assert.Equal("testContent", result.Content);
        //    Assert.Equal("Icaka99", result.UserUserName);
        //    Assert.Equal("Icaka99", result.User.Id);
        //    Assert.Equal(2, result.CategoryId);
        // }
    }
}
