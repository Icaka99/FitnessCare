namespace FitnessCare.Web.ViewModels.Blog
{
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Web.Infrastructure;
    using Microsoft.AspNetCore.Http;

    public class EditArticleInputModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Title must contain a minimum of 2 characters!")]
        [MaxLength(80, ErrorMessage = "Title maximum number of charachters is 80!")]
        [Display(Name = "Title of the Article")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content of the Article")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }

        public IFormFile PictureFile { get; set; }
    }
}
