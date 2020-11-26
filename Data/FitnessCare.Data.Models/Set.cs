namespace FitnessCare.Data.Models
{
    using System.Collections.Generic;

    using FitnessCare.Data.Common.Models;

    public class Set : BaseDeletableModel<int>
    {
        public Set()
        {
            this.Reps = new HashSet<Rep>();
        }

        public int Count { get; set; }

        public int ExerciseId { get; set; }

        public virtual Exercise Exercise { get; set; }

        public virtual ICollection<Rep> Reps { get; set; }
    }
}
