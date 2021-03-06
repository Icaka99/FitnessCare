﻿namespace FitnessCare.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessCare.Web.ViewModels.Blog;

    public interface IArticleService
    {
        int GetCount();

        IEnumerable<ArticleViewModel> GetSearchedArticles(SearchStringInputModel input, int page, int itemsPerPage = 1);

        IEnumerable<ArticleViewModel> GetArticles(int page, int itemsPerPage = 1);

        IEnumerable<ArticleViewModel> GetAllOrderedArticles();

        Task CreateAsync(AddArticleInputModel input, string id);

        ArticleViewModel GetDetails(int id);

        void Delete(int id);

        IEnumerable<ArticleViewModel> GetRandomArticles(int count);

        Task UpdateAsync(int id, EditArticleInputModel model);
    }
}
