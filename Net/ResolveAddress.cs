using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Net
{
    [Activity("Resolve a ip to a hostname")]
    public class ResolveAddress : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("IP Address");
            designer.AddOutput("Host Name");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            IPHostEntry host = Dns.GetHostEntry(request.Inputs["IP Address"].AsString());
            response.PublishRange("Host Name", host.HostName);
        }

    }

}
