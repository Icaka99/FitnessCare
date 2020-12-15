namespace FitnessCare.Web.ViewModels.Blog
{
    using System;
    using System.Collections.Generic;

    using FitnessCare.Web.ViewModels.Forum;

    public class ArticlesListViewModel : PagingViewModel
    {
        public IEnumerable<ArticleViewModel> Articles { get; set; }

        public IEnumerable<ArticleViewModel> OrderedArticles { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public string SearchString { get; set; }
    }
}
