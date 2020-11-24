namespace FitnessCare.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    using FitnessCare.Web.Infrastructure;

    public class ContactFormViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please insert your Names!")]
        [Display(Name = "Your names")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please insert your email!")]
        [EmailAddress(ErrorMessage = "Please insert valid email!")]
        [Display(Name = "Your email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please insert a title of the message!")]
        [StringLength(100, ErrorMessage = "Title length must be atleast {2} and no more than {1} symbols!", MinimumLength = 5)]
        [Display(Name = "Title of the message")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please insert yout message!")]
        [StringLength(10000, ErrorMessage = "Message must be atleast {2} symbols!", MinimumLength = 20)]
        [Display(Name = "Your message")]
        public string Content { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }
    }
}
