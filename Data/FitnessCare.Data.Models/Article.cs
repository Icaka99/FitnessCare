namespace FitnessCare.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Data.Common.Models;

    public class Article : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(80)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
