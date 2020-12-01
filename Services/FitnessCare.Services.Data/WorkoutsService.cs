namespace FitnessCare.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using FitnessCare.Web.ViewModels.Workouts;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class WorkoutsService : IWorkoutsService
    {
        private readonly ApplicationDbContext db;

        public WorkoutsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(WorkoutInputModel model, string userId)
        {
            var workout = new Workout
            {
                Date = model.Date.ToString(),
                Duration = TimeSpan.FromMinutes(model.Duration),
                TypeId = model.TypeId,
                UserId = userId,
            };

            await this.db.Workouts.AddAsync(workout);
            await this.db.SaveChangesAsync();

            model.Id = workout.Id;
        }

        public WorkoutInputModel AssignWorkoutTypes(WorkoutInputModel model)
        {
            model.Types = this.db.WorkoutTypes
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                });

            return model;
        }

        public ExerciseInputModel AssignMuscleGroups(ExerciseInputModel model)
        {
            model.MuscleGroups = this.db.MuscleGroups
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                });

            return model;
        }

        public async Task CreateExerciseAsync(ExerciseInputModel input)
        {
            var exercise = new Exercise
            {
                MuscleGroupId = input.MuscleGroupId,
                Name = input.Name,
                WorkoutId = input.WorkoutId,
            };

            foreach (var inputSet in input.Sets)
            {
                var set = new Set
                {
                    Reps = inputSet.Reps,
                };

                exercise.Sets.Add(set);
            }

            var workout = this.db.Workouts.FirstOrDefault(x => x.Id == input.WorkoutId);

            await this.db.WorkoutExercises.AddAsync(new WorkoutExercise
            {
                Exercise = exercise,
                Workout = workout,
            });

            await this.db.Exercises.AddAsync(exercise);
            await this.db.SaveChangesAsync();
        }
    }
}
