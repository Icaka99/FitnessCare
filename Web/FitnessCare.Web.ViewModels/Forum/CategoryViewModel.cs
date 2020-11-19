namespace FitnessCare.Web.ViewModels.Forum
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public string ShortDescription
        {
            get
            {
                return this.Description.Length > 60
                    ? this.Description?.Substring(0, 60) + "..."
                    : this.Description;
            }
        }
    }
}
