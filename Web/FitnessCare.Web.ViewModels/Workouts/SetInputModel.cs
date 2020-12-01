namespace FitnessCare.Web.ViewModels.Workouts
{
    using System.ComponentModel.DataAnnotations;

    public class SetInputModel
    {
        [Required(ErrorMessage = "Number of Reps is required!")]
        public int Reps { get; set; }

        public int ExerciseId { get; set; }
    }
}
