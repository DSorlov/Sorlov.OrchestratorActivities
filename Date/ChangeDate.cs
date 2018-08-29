using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Date
{
    [Activity("Change DateTime")]
    public class IncrementDate : IActivity
    {
        public enum IncrementWhat { Milliseconds, Seconds, Minutes, Hours, Days, Months, Years }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Change What").WithEnumBrowser(typeof(IncrementWhat));
            designer.AddInput("Change");
            designer.AddInput("Date/Time").WithDateTimeBrowser();
            designer.AddOutput("Old Date/Time").AsDateTime();
            designer.AddOutput("New Date/Time").AsDateTime();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            DateTime oldDateTime = request.Inputs["Date/Time"].AsDateTime();
            response.Publish("Old Date/Time", oldDateTime);

            DateTime newDateTime;
            IncrementWhat what = request.Inputs["Increment What"].As<IncrementWhat>();
            IRuntimeValue increment = request.Inputs["Increment"];

            switch (what)
            {
                case IncrementWhat.Milliseconds:
                    newDateTime = oldDateTime.AddMilliseconds(increment.AsDouble());
                    break;

                case IncrementWhat.Seconds:
                    newDateTime = oldDateTime.AddSeconds(increment.AsDouble());
                    break;

                case IncrementWhat.Minutes:
                    newDateTime = oldDateTime.AddMinutes(increment.AsDouble());
                    break;

                case IncrementWhat.Hours:
                    newDateTime = oldDateTime.AddHours(increment.AsDouble());
                    break;

                case IncrementWhat.Days:
                    newDateTime = oldDateTime.AddDays(increment.AsDouble());
                    break;

                case IncrementWhat.Months:
                    newDateTime = oldDateTime.AddMonths(increment.AsInt32());
                    break;

                case IncrementWhat.Years:
                    newDateTime = oldDateTime.AddYears(increment.AsInt32());
                    break;

                default:
                    newDateTime = oldDateTime;
                    break;
            }

            response.Publish("New Date/Time", newDateTime);
        }
    }

}
