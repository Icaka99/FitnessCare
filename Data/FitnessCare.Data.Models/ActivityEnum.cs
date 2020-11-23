namespace FitnessCare.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum ActivityEnum
    {
        [Display(Name = "Basal Metabolic Rate (BRM)")]
        BasalMetabolicRate = 0,
        [Display(Name = "Sedentary - little or no exercise")]
        Sedentary = 1,
        [Display(Name = "Lightly active - exercise/sports (1-3 times/week)")]
        LightlyActive = 2,
        [Display(Name = "Moderately active - exercise/sports (3-5 times/week)")]
        ModeratelyActive = 3,
        [Display(Name = "Very active - exercise/sports (5-6 times/week)")]
        VeryActive = 4,
        [Display(Name = "Extra active - very hard exercise/sports (twice/day)")]
        ExtraActive = 5,
    }
}
