namespace FitnessCare.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessCare.Data;
    using FitnessCare.Data.Models;
    using FitnessCare.Services.Data;
    using FitnessCare.Web.ViewModels.Workouts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SchedulerController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWorkoutsService workoutService;

        public SchedulerController(UserManager<ApplicationUser> userManager, IWorkoutsService workoutService)
        {
            this.userManager = userManager;
            this.workoutService = workoutService;
        }

        public IActionResult Scheduler()
        {
            return this.View();
        }

        public IActionResult AddWorkout()
        {
            var model = new WorkoutInputModel();

            var viewModel = this.workoutService.AssignWorkoutTypesAndMuscleGroups(model);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkout(WorkoutInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);

            model.UserId = userId;

            if (!this.ModelState.IsValid)
            {
                this.workoutService.AssignWorkoutTypesAndMuscleGroups(model);

                return this.View(model);
            }

            await this.workoutService.CreateAsync(model, userId);

            return this.Redirect("/Scheduler/AddWorkout");
        }
    }
}
