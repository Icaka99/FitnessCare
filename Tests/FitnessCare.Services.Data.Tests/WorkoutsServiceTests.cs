namespace FitnessCare.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using FitnessCare.Web.ViewModels.Workouts;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class WorkoutsServiceTests
    {
        [Fact]
        public async Task CreateMethodShouldAddCorrectNewWorkoutToDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var workoutService = new WorkoutsService(dbContext);

            var workoutToAdd = new WorkoutInputModel
            {
                Date = DateTime.Now.Date,
                Duration = 180,
                TypeId = 2,
            };

            await workoutService.CreateAsync(workoutToAdd, "Icaka99");

            Assert.True(dbContext.Workouts.Any());
            Assert.Equal(DateTime.Now.Date, dbContext.Workouts.FirstAsync().Result.Date);
            Assert.Equal(180, dbContext.Workouts.FirstAsync().Result.Duration.TotalMinutes);
            Assert.Equal(2, dbContext.Workouts.FirstAsync().Result.TypeId);
            Assert.Equal("Icaka99", dbContext.Workouts.FirstAsync().Result.UserId);
        }

        [Fact]
        public async Task AssignWorkoutTypesShouldAssignCorrectWorkoutTypesToWorkout()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var workoutService = new WorkoutsService(dbContext);

            var workoutType = new WorkoutType
            {
                Name = "testName",
            };

            await dbContext.WorkoutTypes.AddAsync(workoutType);
            await dbContext.SaveChangesAsync();

            var workoutToAdd = new WorkoutInputModel
            {
                Date = DateTime.Now.Date,
                Duration = 180,
                TypeId = 1,
                UserId = "Icaka99",
            };

            var result = workoutService.AssignWorkoutTypes(workoutToAdd);

            Assert.NotNull(result.Types);
            Assert.Equal("testName", result.Types.FirstOrDefault().Text);
            Assert.Equal("1", result.Types.FirstOrDefault().Value);
        }

        [Fact]
        public async Task AssignMuscleGroupsShouldAssignCorrectMuscleGroupsToExercise()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var workoutService = new WorkoutsService(dbContext);

            var muscleGroup = new MuscleGroup
            {
                Name = "testName",
            };

            await dbContext.MuscleGroups.AddAsync(muscleGroup);
            await dbContext.SaveChangesAsync();

            var exerciseToAdd = new ExerciseInputModel();

            var result = workoutService.AssignMuscleGroups(exerciseToAdd);

            Assert.NotNull(result.MuscleGroups);
            Assert.Equal("testName", result.MuscleGroups.FirstOrDefault().Text);
            Assert.Equal("1", result.MuscleGroups.FirstOrDefault().Value);
        }

        [Fact]
        public async Task CreateExerciseMethodShouldAddCorrectNewExerciseToDbAndWorkout()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var workoutService = new WorkoutsService(dbContext);

            var workoutToAdd = new WorkoutInputModel
            {
                Date = DateTime.Now.Date,
                Duration = 180,
                TypeId = 2,
            };

            await workoutService.CreateAsync(workoutToAdd, "Icaka99");

            var sets = new List<SetInputModel>
            {
                new SetInputModel { ExerciseId = 2, Reps = 1, },
            };

            var exerciseToAdd = new ExerciseInputModel
            {
                MuscleGroupId = 1,
                Name = "testName",
                WorkoutId = 1,
                Sets = sets,
            };

            await workoutService.CreateExerciseAsync(exerciseToAdd);

            Assert.True(dbContext.Exercises.Any());
            Assert.Equal(2, dbContext.Exercises.FirstOrDefault().Id);
            Assert.Equal(1, dbContext.Exercises.FirstAsync().Result.MuscleGroupId);
            Assert.Equal("testName", dbContext.Exercises.FirstAsync().Result.Name);
            Assert.True(dbContext.Exercises.FirstAsync().Result.Sets.Any());
            Assert.Equal(1, dbContext.Exercises.FirstAsync().Result.Sets.FirstOrDefault().Reps);
            Assert.Equal(2, dbContext.Exercises.FirstAsync().Result.Sets.FirstOrDefault().ExerciseId);
        }

        [Fact]
        public async Task GetWorkoutIdFromDateShouldReturnCorrectWorkoutId()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var workoutService = new WorkoutsService(dbContext);

            var workoutToAdd = new WorkoutInputModel
            {
                Date = DateTime.Now.Date,
            };

            await workoutService.CreateAsync(workoutToAdd, "Icaka99");

            var result = workoutService.GetWorkoutIdFromDate(DateTime.Now.Date.ToString("yy-MM-dd"));

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetEventsShouldReturnCorrectEventsForWorkout()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var workoutService = new WorkoutsService(dbContext);

            var type = new WorkoutType
            {
                Name = "testName",
            };

            var workout = new Workout
            {
                Date = DateTime.Now.Date,
                Type = type,
                UserId = "Icaka99",
            };

            await dbContext.AddAsync(workout);
            await dbContext.SaveChangesAsync();

            var result = workoutService.GetEvents("Icaka99");

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("testName", result.FirstOrDefault().Title);
            Assert.Equal(DateTime.Now.Date, result.FirstOrDefault().Date);
        }

        [Fact]
        public async Task GetDetailsShouldReturnAllCorrectDetailsAboutWorkoutById()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var workoutService = new WorkoutsService(dbContext);

            var user = new ApplicationUser { UserName = "Icaka99", Id = "Icaka99" };

            var type = new WorkoutType { Name = "testWorkoutTypeName", Id = 2 };

            var muscleGroup = new MuscleGroup { Name = "testMuscleGroupName", Id = 2 };

            var sets = new List<Set>
            {
                new Set { ExerciseId = 2, Reps = 1, },
            };

            var exercises = new List<Exercise>()
            {
                new Exercise
                {
                    Id = 2,
                    Name = "testExerciseName",
                    MuscleGroup = muscleGroup,
                    WorkoutId = 1,
                    Sets = sets,
                    MuscleGroupId = 2,
                },
            };

            var workout = new Workout
            {
                Date = DateTime.Now.Date,
                Duration = TimeSpan.FromMinutes(180),
                TypeId = 2,
                User = user,
                UserId = user.Id,
                Type = type,
                Exercises = exercises,
            };

            await dbContext.AddAsync(workout);
            await dbContext.SaveChangesAsync();

            var result = workoutService.GetDetails(1);

            Assert.NotNull(result);
            Assert.Equal(DateTime.Now.Date, result.Date);
            Assert.Equal(180, result.Duration);
            Assert.Equal(1, result.Id);
            Assert.Equal("Icaka99", result.UserUserName);
            Assert.Equal("testWorkoutTypeName", result.WorkoutType.Name);
            Assert.Equal(2, result.WorkoutType.Id);
            Assert.Equal(2, result.Exercises.FirstOrDefault().Id);
            Assert.Equal("testMuscleGroupName", result.Exercises.FirstOrDefault().MuscleGroup.Name);
            Assert.Equal(2, result.Exercises.FirstOrDefault().MuscleGroup.Id);
            Assert.Equal("testExerciseName", result.Exercises.FirstOrDefault().Name);
            Assert.Equal(1, result.Exercises.FirstOrDefault().Sets.FirstOrDefault().Reps);
        }
    }
}
