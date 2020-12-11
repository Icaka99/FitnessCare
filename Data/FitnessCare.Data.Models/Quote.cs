namespace FitnessCare.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Data.Common.Models;

    public class Quote : BaseDeletableModel<int>
    {
        [Required]
        public string Content { get; set; }

        public virtual Author Author { get; set; }
    }
}
