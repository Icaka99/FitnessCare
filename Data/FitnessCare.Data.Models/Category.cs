namespace FitnessCare.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Posts = new HashSet<Post>();
        }

        [MaxLength(80)]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
