﻿namespace FitnessCare.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public int? ParentId { get; set; }

        public Comment Parent { get; set; }

        [Required]
        public string Content { get; set; }

        public string UserId { get; set; }

        public string UserUsername { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
