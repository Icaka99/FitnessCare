namespace FitnessCare.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCommentInputModel
    {
        [Required]
        public string Content { get; set; }

        public int ArticleId { get; set; }

        public int ParentId { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }
    }
}
