namespace FitnessCare.Web.ViewModels.Forum
{
    using System;
    using System.Collections.Generic;

    using FitnessCare.Data.Models;

    public class PostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }

        public int CommentsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        public ApplicationUser User { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
