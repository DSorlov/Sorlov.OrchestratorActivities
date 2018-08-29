using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Net
{
    [Activity("Resolve a hostname to a ip")]
    public class ResolveHostname : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Host Name");
            designer.AddOutput("IP Address");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            IPHostEntry host = Dns.GetHostEntry(request.Inputs["Host Name"].AsString());
            response.PublishRange("IP Address", host.AddressList);
        }
    }

}
