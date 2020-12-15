namespace FitnessCare.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using FitnessCare.Web.ViewModels.Forum;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CategoriesServiceTests
    {
        [Fact]
        public async Task CreateMethodShouldAddCorrectNewCategoryToDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var categoryService = new CategoryService(dbContext, null);

            var categoryToAdd = new AddCategoryInputModel
            {
                Name = "testName",
                Description = "testDescription",
            };

            await categoryService.CreateAsync(categoryToAdd);

            Assert.NotNull(dbContext.Categories.FirstOrDefaultAsync());
            Assert.Equal("testName", dbContext.Categories.FirstAsync().Result.Name);
            Assert.Equal("testDescription", dbContext.Categories.FirstAsync().Result.Description);
        }

        [Fact]
        public async Task DeleteMethodShouldDeleteCategorieById()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var categoryService = new CategoryService(dbContext, null);

            var category = new Category
            {
                Id = 5,
                Name = "testName",
                Description = "testDescription",
                ImageURL = "testImage",
            };

            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            categoryService.Delete(5);

            Assert.False(dbContext.Categories.Any());
        }

        [Fact]
        public async Task GetAllShouldReturnAllCategories()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var categoryService = new CategoryService(dbContext, null);

            var firstCategory = new Category
            {
                Id = 5,
                Name = "testName",
                Description = "testDescription",
                ImageURL = "testImage",
            };

            var secondCategory = new Category
            {
                Id = 4,
                Name = "testName",
                Description = "testDescription",
                ImageURL = "testImage",
            };

            await dbContext.Categories.AddAsync(firstCategory);
            await dbContext.Categories.AddAsync(secondCategory);
            await dbContext.SaveChangesAsync();

            var result = categoryService.GetAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("testName", result.FirstOrDefault().Name);
            Assert.Equal("testDescription", result.FirstOrDefault().Description);
            Assert.Equal("testImage", result.FirstOrDefault().ImageURL);
            Assert.Equal(5, result.FirstOrDefault().Id);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectCountOfCategoriesInDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var categoryService = new CategoryService(dbContext, null);

            var firstCategory = new Category();

            var secondCategory = new Category();

            await dbContext.Categories.AddAsync(firstCategory);
            await dbContext.Categories.AddAsync(secondCategory);
            await dbContext.SaveChangesAsync();

            var result = categoryService.GetCount();

            Assert.Equal(2, result);
        }

        [Fact]
        public async Task GetDetailsShouldReturnCorrectDetailsAboutCategoryById()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var categoryService = new CategoryService(dbContext, null);

            var firstCategory = new Category
            {
                Id = 5,
                Name = "testName",
                Description = "testDescription",
                ImageURL = "testImage",
            };

            var secondCategory = new Category
            {
                Id = 4,
                Name = "testName1",
                Description = "testDescription1",
                ImageURL = "testImage1",
            };

            await dbContext.Categories.AddAsync(firstCategory);
            await dbContext.Categories.AddAsync(secondCategory);
            await dbContext.SaveChangesAsync();

            var result = categoryService.GetDetails(5);

            Assert.NotNull(result);
            Assert.Equal("testName", result.Name);
            Assert.Equal("testDescription", result.Description);
            Assert.Equal("testImage", result.ImageURL);
        }

        [Fact]
        public async Task GetRandomCategoriesShouldReturnCorrectCountCategories()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var categoryService = new CategoryService(dbContext, null);

            var firstCategory = new Category
            {
                Id = 5,
                Name = "testName",
                Description = "testDescription",
                ImageURL = "testImage",
            };

            var secondCategory = new Category
            {
                Id = 4,
                Name = "testName",
                Description = "testDescription",
                ImageURL = "testImage",
            };

            var thirdCategory = new Category
            {
                Id = 3,
                Name = "testName",
                Description = "testDescription",
                ImageURL = "testImage",
            };

            await dbContext.Categories.AddAsync(firstCategory);
            await dbContext.Categories.AddAsync(secondCategory);
            await dbContext.Categories.AddAsync(thirdCategory);
            await dbContext.SaveChangesAsync();

            var result = categoryService.GetRandomCategories(2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("testName", result.FirstOrDefault().Name);
            Assert.Equal("testDescription", result.FirstOrDefault().Description);
            Assert.Equal("testImage", result.FirstOrDefault().ImageURL);
        }
    }
}
