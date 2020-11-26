namespace FitnessCare.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Data.Common.Models;

    public class WorkoutType : BaseDeletableModel<int>
    {
        public WorkoutType()
        {
            this.Workouts = new HashSet<Workout>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }
    }
}
