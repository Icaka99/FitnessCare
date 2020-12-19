namespace FitnessCare.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Data.Common.Models;

    public class Exercise : BaseDeletableModel<int>
    {
        public Exercise()
        {
            this.Sets = new HashSet<Set>();
        }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        public int MuscleGroupId { get; set; }

        public MuscleGroup MuscleGroup { get; set; }

        public int WorkoutId { get; set; }

        public virtual Workout Workout { get; set; }

        public virtual ICollection<Set> Sets { get; set; }
    }
}
