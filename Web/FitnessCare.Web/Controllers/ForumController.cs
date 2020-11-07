namespace FitnessCare.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ForumController : BaseController
    {
        public IActionResult Forum()
        {
            return this.View();
        }
    }
}
