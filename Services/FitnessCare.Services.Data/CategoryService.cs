namespace FitnessCare.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using FitnessCare.Web.ViewModels.Forum;

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext db;

        public CategoryService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(AddCategoryInputModel input)
        {
            var dbCategory = new Category
            {
                Name = input.Name,
                Description = input.Description,
                ImageURL = input.ImageURL,
            };
            await this.db.Categories.AddAsync(dbCategory);
            await this.db.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var category = this.db.Categories.Where(x => x.Id == id).FirstOrDefault();
            category.IsDeleted = true;
            this.db.SaveChanges();
        }

        public int GetCount()
        {
            return this.db.Categories.Count();
        }
    }
}
