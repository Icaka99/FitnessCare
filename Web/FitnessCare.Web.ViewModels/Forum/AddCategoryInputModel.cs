namespace FitnessCare.Web.ViewModels.Forum
{
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Web.Infrastructure;
    using Microsoft.AspNetCore.Http;

    public class AddCategoryInputModel
    {
        [MaxLength(80)]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile PictureFile { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }
    }
}
