namespace FitnessCare.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessCare.Data.Models;
    using FitnessCare.Web.ViewModels.Workouts;

    public interface IWorkoutsService
    {
        Task CreateAsync(WorkoutInputModel input, string userId);

        WorkoutInputModel AssignWorkoutTypes(WorkoutInputModel model);

        ExerciseInputModel AssignMuscleGroups(ExerciseInputModel model);

        Task CreateExerciseAsync(ExerciseInputModel input);

        WorkoutViewModel GetDetails(int id);

        int GetWorkoutIdFromDate(string date);

        List<CalendarEvent> GetEvents();
    }
}
