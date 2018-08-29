using System;
using System.Collections;
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
    [Activity("Enumerate all PSSessions")]
    public class EnumeratePSSession : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddOutput("Session count");
            designer.AddCorellatedData(typeof(SessionInfoData));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            try
            {
                IRunspaceManager runspaceManager = PSRunspaceManger.CreateClient();
                Dictionary<string, string> result = runspaceManager.GetRunspaces();

                List<SessionInfoData> sessions = new List<SessionInfoData>();
                foreach(KeyValuePair<string,string> pair in result)
                    sessions.Add(new SessionInfoData(pair.Key, pair.Value));

                response.WithFiltering().PublishRange(sessions);
                response.Publish("Session count", sessions.Count);

            }
            catch (Exception)
            {
                throw new Exception("An error occured while enumerating sessions");
            }
        }
    }
}
