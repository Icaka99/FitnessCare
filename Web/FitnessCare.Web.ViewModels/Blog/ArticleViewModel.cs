namespace FitnessCare.Web.ViewModels.Blog
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;

    using Ganss.XSS;

    public class ArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        public string ShortContent
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Content, @"<[^>]+>", string.Empty));

                return content.Length > 60
                    ? content?.Substring(0, 60) + "..."
                    : content;
            }
        }
    }
}
