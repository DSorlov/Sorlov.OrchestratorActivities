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
    [Activity("Get variable value")]
    public class SetVariableValue : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Session name");
            designer.AddInput("Variable name");
            designer.AddInput("Variable value");

            designer.AddOutput("Session name");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string command = string.Format("Set-Variable {0} \"{1}\" ", request.Inputs["Variable name"].AsString(), request.Inputs["Variable value"].AsString());
            IRunspaceManager runspaceManager = PSRunspaceManger.CreateClient();
            RemoteOperationResult result = runspaceManager.ExecuteCommand(request.Inputs["Session name"].AsString().ToUpper(), command);

            if (!result.Success)
                throw new Exception(result.Message);

            response.Publish("Session name", request.Inputs["Session Name"].AsString().ToUpper());
            response.Publish("Variable value", result.Message);
        }
    }
}
