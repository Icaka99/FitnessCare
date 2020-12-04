namespace FitnessCare.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Data.Common.Models;

    public class Workout : BaseDeletableModel<int>
    {
        public Workout()
        {
            this.Exercises = new HashSet<Exercise>();
        }

        public int TypeId { get; set; }

        public WorkoutType Type { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public TimeSpan Duration { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
