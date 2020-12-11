namespace FitnessCare.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessCare.Data.Models;
    using FitnessCare.Services.Data;
    using FitnessCare.Web.ViewModels.Scheduler;
    using FitnessCare.Web.ViewModels.Workouts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SchedulerController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWorkoutsService workoutService;

        public SchedulerController(UserManager<ApplicationUser> userManager, IWorkoutsService workoutService)
        {
            this.userManager = userManager;
            this.workoutService = workoutService;
        }

        public async Task<IActionResult> Scheduler([FromRoute]int year, int month)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            string userId = string.Empty;

            if (this.User.Identity.IsAuthenticated)
            {
                userId = user.Id;
            }

            if (month < 1)
            {
                year--;
                month = 12;
            }
            else if (month > 12)
            {
                year++;
                month = 1;
            }

            var events = new SchedulerViewModel
            {
                Events = this.workoutService.GetEvents(userId),
                Month = month,
                Year = year,
            };

            return this.View(events);
        }

        [Authorize]
        public IActionResult AddWorkout()
        {
            var model = new WorkoutInputModel();

            var viewModel = this.workoutService.AssignWorkoutTypes(model);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddWorkout(WorkoutInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);

            model.UserId = userId;

            if (!this.ModelState.IsValid)
            {
                this.workoutService.AssignWorkoutTypes(model);

                return this.View(model);
            }

            await this.workoutService.CreateAsync(model, userId);

            this.TempData["Message"] = "Workout added successfully";

            return this.RedirectToAction("AddExerciseToWorkout", "Scheduler", new { id = model.Id });
        }

        [Authorize]
        public IActionResult AddExerciseToWorkout()
        {
            var model = new ExerciseInputModel();

            var viewModel = this.workoutService.AssignMuscleGroups(model);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddExerciseToWorkout(ExerciseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.workoutService.AssignMuscleGroups(model);

                return this.View(model);
            }

            await this.workoutService.CreateExerciseAsync(model);

            this.TempData["Message"] = "Exercise added successfully to Workout";

            return this.RedirectToAction("AddExerciseToWorkout", "Scheduler", new { id = model.WorkoutId });
        }

        [Authorize]
        public IActionResult Workout([FromRoute] string id)
        {
            int workoutId = this.workoutService.GetWorkoutIdFromDate(id);

            var workout = this.workoutService.GetDetails(workoutId);
            if (workout == null)
            {
                return this.NotFound();
            }

            return this.View(workout);
        }
    }
}
