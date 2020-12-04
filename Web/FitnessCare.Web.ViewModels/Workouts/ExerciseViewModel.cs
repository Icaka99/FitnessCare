namespace FitnessCare.Web.ViewModels.Workouts
{
    using System.Collections.Generic;

    using FitnessCare.Data.Models;

    public class ExerciseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MuscleGroup MuscleGroup { get; set; }

        public IEnumerable<SetViewModel> Sets { get; set; }
    }
}
