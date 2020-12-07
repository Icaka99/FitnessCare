namespace FitnessCare.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessCare.Web.ViewModels.Forum;

    public interface ICategoryService
    {
        int GetCount();

        Task CreateAsync(AddCategoryInputModel input);

        void Delete(int id);

        CategoryViewModel GetDetails(int id);

        IEnumerable<CategoryViewModel> GetAll();

        IEnumerable<CategoryViewModel> GetRandomCategories(int count);
    }
}
