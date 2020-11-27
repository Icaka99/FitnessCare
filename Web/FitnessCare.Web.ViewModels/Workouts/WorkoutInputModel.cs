namespace FitnessCare.Web.ViewModels.Workouts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class WorkoutInputModel
    {
        [Display(Name = "Workout type")]
        public int TypeId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Range(0, 24 * 60)]
        [Display(Name = "Duration(in minutes)")]
        public int Duration { get; set; }

        public string UserId { get; set; }

        [Required]
        public ICollection<Exercise> Exercises { get; set; }

        public IEnumerable<SelectListItem> Types { get; set; }

        public IEnumerable<SelectListItem> MuscleGroups { get; set; }
    }
}
