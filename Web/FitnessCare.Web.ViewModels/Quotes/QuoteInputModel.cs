namespace FitnessCare.Web.ViewModels.Quotes
{
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Web.Infrastructure;

    public class QuoteInputModel
    {
        [Required]
        [Display(Name = "Content of the Quote")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Author Name must contain a minimum of 2 characters!")]
        [MaxLength(80, ErrorMessage = "Author Name maximum number of charachters is 80!")]
        [Display(Name = "Author of the Quote")]
        public string AuthorName { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }
    }
}
