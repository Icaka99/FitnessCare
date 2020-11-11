namespace FitnessCare.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessCare.Web.ViewModels.Blog;

    public interface IArticleService
    {
        int GetCount();

        IEnumerable<ArticleViewModel> GetAll();

        IEnumerable<ArticleViewModel> GetArticles(int? take, int skip);

        IEnumerable<ArticleViewModel> GetAllOrderedArticles();

        Task CreateAsync(AddArticleInputModel input, string id);

        ArticleViewModel GetDetails(int id);

        void Delete(int id);
    }
}
