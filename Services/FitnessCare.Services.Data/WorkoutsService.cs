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

            foreach (var inputExercise in model.Exercises)
            {
                var exercise = this.db.Exercises.FirstOrDefault(x => x.Name == inputExercise.Name);

                if (exercise == null)
                {
                    exercise = new Exercise
                    {
                        Name = inputExercise.Name,
                        MuscleGroupId = inputExercise.MuscleGroupId,
                    };
                }

                foreach (var inputSet in inputExercise.Sets)
                {
                    var set = this.db.Sets.FirstOrDefault(x => x.Reps == inputSet.Reps);

                    if (set == null)
                    {
                        set = new Set
                        {
                            Reps = inputSet.Reps,
                        };
                    }

                    exercise.Sets.Add(set);
                }

                workout.Exercises.Add(exercise);

                await this.db.WorkoutExercises.AddAsync(new WorkoutExercise
                {
                    Exercise = exercise,
                    Workout = workout,
                });
            }

            await this.db.Workouts.AddAsync(workout);
            await this.db.SaveChangesAsync();
        }

        public WorkoutInputModel AssignWorkoutTypesAndMuscleGroups(WorkoutInputModel model)
        {
            model.Types = this.db.WorkoutTypes
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                });

            model.MuscleGroups = this.db.MuscleGroups
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                });

            return model;
        }
    }
}
