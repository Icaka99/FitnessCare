namespace FitnessCare.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Data.Common.Models;

    public class Author : BaseDeletableModel<int>
    {
        public Author()
        {
            this.Quotes = new HashSet<Quote>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Quote> Quotes { get; set; }
    }
}
