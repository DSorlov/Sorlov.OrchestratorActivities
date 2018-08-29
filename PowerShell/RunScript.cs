using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Remoting;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Security;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Sorlov.OrchestratorActivities.PSRunspaceManager.Dto;
using Sorlov.OrchestratorActivities.PSRunspaceManager.Interfaces;

namespace Sorlov.OrchestratorActivities.PowerShell
{
    [Activity("Run PowerShell script")]
    public class RunScript : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Session name");
            designer.AddInput("Script code");

            designer.AddOutput("Session name");
            designer.AddOutput("Script result");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            IRunspaceManager runspaceManager = PSRunspaceManger.CreateClient();
            RemoteOperationResult result = runspaceManager.ExecuteCommand(request.Inputs["Session name"].AsString().ToUpper(), request.Inputs["Script code"].AsString());

            if (!result.Success)
                throw new Exception(result.Message);

            response.Publish("Session name", request.Inputs["Session name"].AsString().ToUpper());
            response.Publish("Script result", result.Message);
        }
    }
}
