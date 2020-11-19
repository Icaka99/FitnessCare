namespace FitnessCare.Web.ViewModels.Forum
{
    using System.ComponentModel.DataAnnotations;

    public class AddPostInputModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Title must contain a minimum of 2 characters!")]
        [MaxLength(80, ErrorMessage = "Title maximum number of charachters is 80!")]
        [Display(Name = "Title of the Post")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content of the Post")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public int CategoryId { get; set; }
    }
}
