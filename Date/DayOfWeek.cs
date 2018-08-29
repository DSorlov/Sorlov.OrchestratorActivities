using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Date
{
    [Activity("Days of the week")]
    public class DayOfTheWeek
    {
        private DateTime date = DateTime.Now;

        [ActivityInput]
        public DateTime Date
        {
            set { date = value; }
        }
        [ActivityOutput(Description = "The day of the week (Sunday = 0, Saturday = 6")]
        public DayOfWeek Day
        {
            get { return date.DayOfWeek; }
        }
    }

}
