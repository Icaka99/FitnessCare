namespace FitnessCare.Data.Models
{
    using System;

    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        [MaxLength(80)]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }
    }
}
