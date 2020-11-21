namespace FitnessCare.Web.ViewModels.Blog
{
    using System;
    using System.Collections.Generic;

    public class ArticlesListViewModel : PagingViewModel
    {
        public IEnumerable<ArticleViewModel> Articles { get; set; }

        public IEnumerable<ArticleViewModel> OrderedArticles { get; set; }
    }
}
