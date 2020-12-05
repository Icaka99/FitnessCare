namespace FitnessCare.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitnessCare.Data.Models;
    using FitnessCare.Services.Data;
    using FitnessCare.Web.ViewModels.Workouts;
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

        public IActionResult Scheduler()
        {
            var events = this.workoutService.GetEvents();

            return this.View(events);
        }

        public IActionResult AddWorkout()
        {
            var model = new WorkoutInputModel();

            var viewModel = this.workoutService.AssignWorkoutTypes(model);

            return this.View(viewModel);
        }

        [HttpPost]
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

        public IActionResult AddExerciseToWorkout()
        {
            var model = new ExerciseInputModel();

            var viewModel = this.workoutService.AssignMuscleGroups(model);

            return this.View(viewModel);
        }

        [HttpPost]
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
