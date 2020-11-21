namespace FitnessCare.Web.ViewModels
{
    using System;

    public class PagingViewModel
    {
        public int ArticlesCount { get; set; }

        public int PagesCount => (int)Math.Ceiling((double)this.ArticlesCount / this.ItemsPerPage);

        public int PageNumber { get; set; }

        public int ItemsPerPage { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;
    }
}
