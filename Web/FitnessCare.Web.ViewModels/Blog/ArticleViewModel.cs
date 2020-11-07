namespace FitnessCare.Web.ViewModels
{
    using FitnessCare.Data.Models;
    using FitnessCare.Services.Mapping;

    public class ArticleViewModel : IMapFrom<Article>
    {
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
