namespace FitnessCare.Data.Models
{
    using FitnessCare.Data.Common.Models;

    public class Set : BaseDeletableModel<int>
    {
        public int Reps { get; set; }

        public int ExerciseId { get; set; }

        public virtual Exercise Exercise { get; set; }
    }
}
