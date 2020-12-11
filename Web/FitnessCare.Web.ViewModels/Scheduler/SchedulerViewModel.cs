namespace FitnessCare.Web.ViewModels.Scheduler
{
    using System.Collections.Generic;

    using FitnessCare.Data.Models;

    public class SchedulerViewModel
    {
        public IEnumerable<CalendarEvent> Events { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
