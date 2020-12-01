namespace FitnessCare.Web.Controllers
{
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
            return this.View();
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

            return this.RedirectToAction("AddExerciseToWorkout", "Scheduler", new { id = model.WorkoutId });
        }
    }
}
