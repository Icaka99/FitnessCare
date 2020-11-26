namespace FitnessCare.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Data.Common.Models;

    public class MuscleGroup : BaseDeletableModel<int>
    {
        public MuscleGroup()
        {
            this.Exercises = new HashSet<Exercise>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
