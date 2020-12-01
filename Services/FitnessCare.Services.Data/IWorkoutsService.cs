﻿namespace FitnessCare.Services.Data
{
    using System.Threading.Tasks;

    using FitnessCare.Web.ViewModels.Workouts;

    public interface IWorkoutsService
    {
        Task CreateAsync(WorkoutInputModel input, string userId);

        WorkoutInputModel AssignWorkoutTypes(WorkoutInputModel model);

        ExerciseInputModel AssignMuscleGroups(ExerciseInputModel model);

        Task CreateExerciseAsync(ExerciseInputModel input);
    }
}
