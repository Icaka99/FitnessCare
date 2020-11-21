namespace FitnessCare.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessCare.Web.ViewModels.Blog;

    public interface IArticleService
    {
        int GetCount();

        IEnumerable<ArticleViewModel> GetArticles(int page, int itemsPerPage = 1);

        IEnumerable<ArticleViewModel> GetAllOrderedArticles();

        Task CreateAsync(AddArticleInputModel input, string id);

        ArticleViewModel GetDetails(int id);

        void Delete(int id);
    }
}
