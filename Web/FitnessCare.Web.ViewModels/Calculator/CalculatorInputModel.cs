namespace FitnessCare.Web.ViewModels.Calculator
{
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Data.Models;

    public class CalculatorInputModel
    {
        [Required]
        public bool Gender { get; set; }

        [Required]
        [Range(0, 500, ErrorMessage = "Invalid weight!")]
        public int Weight { get; set; }

        [Required]
        [Range(0, 300, ErrorMessage = "Invalid height!")]
        public int Height { get; set; }

        [Required]
        [Range(0, 150, ErrorMessage = "Invalid age!")]
        public int Age { get; set; }

        [Required]
        public ActivityEnum Activity { get; set; }
    }
}
