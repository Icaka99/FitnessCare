namespace FitnessCare.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using FitnessCare.Web;
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
                Date = model.Date,
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

        public WorkoutViewModel GetDetails(int id)
        {
            var workout = this.db.Workouts.Where(x => x.Id == id)
                .Select(x => new WorkoutViewModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    Duration = (int)x.Duration.TotalMinutes,
                    User = x.User,
                    UserUserName = x.User.UserName,
                    WorkoutType = x.Type,
                    Exercises = this.db.Exercises
                        .Where(p => p.WorkoutId == x.Id)
                        .OrderBy(x => x.CreatedOn)
                        .Select(q => new ExerciseViewModel
                        {
                            Id = q.Id,
                            Name = q.Name,
                            MuscleGroup = q.MuscleGroup,
                            Sets = this.db.Sets.Where(r => r.ExerciseId == q.Id)
                            .Select(e => new SetViewModel
                            {
                                Reps = e.Reps,
                            }).ToList(),
                        })
                        .ToList(),
                })
                .FirstOrDefault();

            return workout;
        }

        public int GetWorkoutIdFromDate(string date)
        {
            var workouts = this.db.Workouts.Select(x => new
            {
                Id = x.Id,
                Date = x.Date,
            }).ToList();

            int id = workouts
                .Where(x => x.Date.ToString("yy-MM-dd") == date)
                .Select(x => x.Id)
                .FirstOrDefault();

            return id;
        }

        public List<CalendarEvent> GetEvents(string userId)
        {
            var workouts = this.db.Workouts
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new CalendarEvent
                {
                    Title = x.Type.Name,
                    Date = x.Date.Date,
                    Type = "success",
                })
                .ToList();

            return workouts;
        }
    }
}
