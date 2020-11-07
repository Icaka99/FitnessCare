namespace FitnessCare.Services.Data
{
    using System.Collections.Generic;

    using FitnessCare.Web.ViewModels.Blog;

    public interface IArticleService
    {
        int GetCount();

        IEnumerable<ArticleViewModel> GetAll();

        IEnumerable<ArticleViewModel> GetAllOrderedArticles();

        void Create(AddArticleInputModel input, string id);

        ArticleViewModel GetDetails(int id);

        void Delete(int id);
    }
}
