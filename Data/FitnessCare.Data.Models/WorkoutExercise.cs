namespace FitnessCare.Data.Models
{
    using FitnessCare.Data.Common.Models;

    public class WorkoutExercise : BaseDeletableModel<int>
    {
        public int ExerciseId { get; set; }

        public Exercise Exercise { get; set; }

        public int WorkoutId { get; set; }

        public Workout Workout { get; set; }
    }
}
