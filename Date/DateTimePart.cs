using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Date
{
    [Activity("Get a specific datetime part")]
    public class DateTimePat : IActivity
    {
        public enum DatePart { Milliseconds, Seconds, Minutes, Hours, Days, Months, Years }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Change What").WithEnumBrowser(typeof(DatePart));
            designer.AddInput("Change");
            designer.AddInput("Date/Time").WithDateTimeBrowser();
            designer.AddOutput("Date/Time Part").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            DateTime dateTime = request.Inputs["Date/Time"].AsDateTime();

            DatePart what = request.Inputs["Increment What"].As<DatePart>();
            IRuntimeValue increment = request.Inputs["Increment"];

            switch (what)
            {
                case DatePart.Milliseconds:
                    response.Publish("Date/Time Part", dateTime.Millisecond.ToString());
                    break;

                case DatePart.Seconds:
                    response.Publish("Date/Time Part", dateTime.Second.ToString());
                    break;

                case DatePart.Minutes:
                    response.Publish("Date/Time Part", dateTime.Minute.ToString());
                    break;

                case DatePart.Hours:
                    response.Publish("Date/Time Part", dateTime.Hour.ToString());
                    break;

                case DatePart.Days:
                    response.Publish("Date/Time Part", dateTime.Day.ToString());
                    break;

                case DatePart.Months:
                    response.Publish("Date/Time Part", dateTime.Month.ToString());
                    break;

                case DatePart.Years:
                    response.Publish("Date/Time Part", dateTime.Year.ToString());
                    break;

                default:
                    response.Publish("Date/Time Part", dateTime.ToString());
                    break;
            }

        }
    }

}
