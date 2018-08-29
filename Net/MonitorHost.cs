using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Net
{
    [Activity, ActivityMonitor(Interval = 15)]
    public class MonitorHost : IActivity
    {
        private static readonly string NAME = "Hostname/Address";
        private static readonly string ROUND_TRIP_TIME = "Round Trip Time";
        private static readonly string STATUS = "Status";

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput(NAME).WithComputerBrowser();
            designer.AddOutput(ROUND_TRIP_TIME).AsNumber();
            designer.AddOutput(STATUS);
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            using (var ping = new Ping())
            {
                PingReply reply = ping.Send(request.Inputs[NAME].AsString());
                response.Publish(STATUS, reply.Status);
                response.Publish(ROUND_TRIP_TIME, reply.RoundtripTime);
            }
        }
    }

}
