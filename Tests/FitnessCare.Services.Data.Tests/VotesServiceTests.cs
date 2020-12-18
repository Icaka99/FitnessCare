namespace FitnessCare.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class VotesServiceTests
    {
        [Fact]
        public async Task GetVotesShouldReturnCorrectSumOfVotes()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var votesService = new VoteService(dbContext);

            var firstVote = new Vote
            {
                ArticleId = 1,
                Id = 1,
                Type = VoteType.UpVote,
                UserId = "Icaka99",
            };

            var secondVote = new Vote
            {
                ArticleId = 1,
                Id = 2,
                Type = VoteType.UpVote,
                UserId = "Icaka099",
            };

            await dbContext.Votes.AddAsync(firstVote);
            await dbContext.Votes.AddAsync(secondVote);
            await dbContext.SaveChangesAsync();

            var result = votesService.GetVotes(1);

            Assert.Equal(2, result);
        }

        [Fact]
        public async Task VoteMethodShouldAddCorrectVoteToDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var votesService = new VoteService(dbContext);

            await votesService.VoteAsync(1, true, "Icaka99");

            Assert.True(dbContext.Votes.Any());
            Assert.Equal(1, dbContext.Votes.FirstOrDefaultAsync().Result.ArticleId);
            Assert.Equal(VoteType.UpVote, dbContext.Votes.FirstOrDefaultAsync().Result.Type);
            Assert.Equal("Icaka99", dbContext.Votes.FirstOrDefaultAsync().Result.UserId);
        }

        [Fact]
        public async Task VoteMethodShouldAddCorrectVoteToDbWhenVoteAlreadyExists()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var votesService = new VoteService(dbContext);

            var vote = new Vote
            {
                ArticleId = 1,
                Id = 1,
                Type = VoteType.UpVote,
                UserId = "Icaka99",
            };

            await dbContext.Votes.AddAsync(vote);
            await dbContext.SaveChangesAsync();

            await votesService.VoteAsync(1, false, "Icaka99");

            Assert.True(dbContext.Votes.Any());
            Assert.Equal(1, dbContext.Votes.FirstOrDefaultAsync().Result.ArticleId);
            Assert.Equal(VoteType.DownVote, dbContext.Votes.FirstOrDefaultAsync().Result.Type);
            Assert.Equal("Icaka99", dbContext.Votes.FirstOrDefaultAsync().Result.UserId);
        }
    }
}
