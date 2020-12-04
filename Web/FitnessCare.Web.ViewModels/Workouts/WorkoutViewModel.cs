namespace FitnessCare.Web.ViewModels.Workouts
{
    using System;
    using System.Collections.Generic;

    using FitnessCare.Data.Models;

    public class WorkoutViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public WorkoutType WorkoutType { get; set; }

        public ApplicationUser User { get; set; }

        public string UserUserName { get; set; }

        public int Duration { get; set; }

        public IEnumerable<ExerciseViewModel> Exercises { get; set; }
    }
}
