﻿namespace FitnessCare.Web.ViewModels.Blog
{
    using System.Collections.Generic;

    public class ArticlesListViewModel
    {
        public IEnumerable<ArticleViewModel> Articles { get; set; }

        public IEnumerable<ArticleViewModel> OrderedArticles { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
