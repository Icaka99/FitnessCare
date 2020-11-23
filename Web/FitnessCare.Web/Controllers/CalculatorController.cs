namespace FitnessCare.Web.Controllers
{
    using FitnessCare.Web.ViewModels.Calculator;
    using Microsoft.AspNetCore.Mvc;

    public class CalculatorController : BaseController
    {
        public IActionResult Calculator()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Calculator(CalculatorInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.View();
        }
    }
}
