namespace FitnessCare.Web.ViewModels.Workouts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Web.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class WorkoutInputModel
    {
        public int Id { get; set; }

        [Display(Name = "Workout type")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Please, enter a Date!")]
        public DateTime Date { get; set; }

        [Range(0, 24 * 60)]
        [Display(Name = "Duration(in minutes)")]
        public int Duration { get; set; }

        public string UserId { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }

        public IEnumerable<SelectListItem> Types { get; set; }
    }
}
