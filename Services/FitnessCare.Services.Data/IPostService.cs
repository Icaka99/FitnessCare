namespace FitnessCare.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessCare.Web.ViewModels.Forum;

    public interface IPostService
    {
        int GetCount();

        Task CreateAsync(AddPostInputModel input);

        void Delete(int id);

        PostViewModel GetDetails(int id);

        IEnumerable<PostViewModel> GetAll();
    }
}
