namespace FitnessCare.Web.ViewModels.Workouts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ExerciseInputModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name minimum length is 3 characters!")]
        [MaxLength(80, ErrorMessage = "Name maximum length is 80!")]
        public string Name { get; set; }

        [Display(Name = "Muscle group")]
        public int MuscleGroupId { get; set; }

        public int MuscleGroup { get; set; }

        public int WorkoutId { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }

        [Required(ErrorMessage = "Please, add atleast 1 set!")]
        public ICollection<SetInputModel> Sets { get; set; }

        public IEnumerable<SelectListItem> MuscleGroups { get; set; }
    }
}
