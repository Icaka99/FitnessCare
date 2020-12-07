namespace FitnessCare.Web.ViewModels.Home
{ 
    using System.Collections.Generic;

    using FitnessCare.Web.ViewModels.Blog;
    using FitnessCare.Web.ViewModels.Forum;

    public class IndexViewModel
    {
        public IEnumerable<ArticleViewModel> RandomArticles { get; set; }

        public IEnumerable<CategoryViewModel> RandomCategories { get; set; }
    }
}
