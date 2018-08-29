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
    [Activity("Remove an established PowerShell session")]
    public class ClosePSSession : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Session name");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            IRunspaceManager runspaceManager = PSRunspaceManger.CreateClient();
            RemoteOperationResult result = runspaceManager.CloseRunspace(request.Inputs["Session name"].AsString().ToUpper());

            if (!result.Success)
                throw new Exception(result.Message);       
        }
    }
}
