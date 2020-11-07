namespace FitnessCare.Web.ViewModels.Blog
{
    using System;

    public class ArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
