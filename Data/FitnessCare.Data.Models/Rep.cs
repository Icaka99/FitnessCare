namespace FitnessCare.Data.Models
{
    using FitnessCare.Data.Common.Models;

    public class Rep : BaseDeletableModel<int>
    {
        public int Count { get; set; }

        public int SetId { get; set; }

        public virtual Set Set { get; set; }
    }
}
