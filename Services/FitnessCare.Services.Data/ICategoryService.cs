namespace FitnessCare.Services.Data
{
    using System.Threading.Tasks;

    using FitnessCare.Web.ViewModels.Forum;

    public interface ICategoryService
    {
        int GetCount();

        Task CreateAsync(AddCategoryInputModel input);

        void Delete(int id);
    }
}
