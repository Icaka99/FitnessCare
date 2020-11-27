namespace FitnessCare.Web.ViewModels.Workouts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ExerciseInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public int MuscleGroup { get; set; }

        public int WorkoutId { get; set; }

        public IEnumerable<SetInputModel> Sets { get; set; }
    }
}
