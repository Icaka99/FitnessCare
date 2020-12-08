namespace FitnessCare.Web.ViewModels.Blog
{
    using System.ComponentModel.DataAnnotations;

    public class SearchStringInputModel
    {
        [Required]
        public string SearchString { get; set; }
    }
}
