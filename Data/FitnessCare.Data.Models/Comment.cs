namespace FitnessCare.Data.Models
{
    using System;

    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
