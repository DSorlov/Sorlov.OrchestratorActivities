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
    [Activity("Establish new PowerShell session")]
    public class NewPSSession : IActivity
    {
        private PowerShellSettings settings = new PowerShellSettings();

        [ActivityConfiguration(Usage = ConfigurationUsage.ExecuteOnly), ActivityOutput]
        public PowerShellSettings Settings
        {
            set { settings = value; }
            get { return settings; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Session name");
            designer.AddInput("Use SSL").WithBooleanBrowser().WithDefaultValue(false);
            designer.AddInput("Load host profile").WithBooleanBrowser().WithDefaultValue(true);
            designer.AddInput("Computer name").WithComputerBrowser().WithDefaultValue("localhost");
            designer.AddInput("Connection port").WithDefaultValue("5985");

            designer.AddOutput("Session name");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SecureString password = new SecureString();
            settings.Password.ToCharArray().ToList().ForEach(password.AppendChar);
            PSCredential creds = new PSCredential(settings.UserName, password);
            
            PSRunspaceCreationData creationData = new PSRunspaceCreationData();
            creationData.Credentials = creds;
            creationData.Hostname = request.Inputs["Computer name"].AsString();
            creationData.Port = request.Inputs["Connection port"].AsInt32();
            creationData.UseSsl = request.Inputs["Use SSL"].AsBoolean();
            creationData.LoadMachineProfile = request.Inputs["Load host profile"].AsBoolean();
            creationData.Name = request.Inputs["Session name"].AsString().ToUpper();

            IRunspaceManager runspaceManager = PSRunspaceManger.CreateClient();
            RemoteOperationResult result = runspaceManager.OpenRunspace(creationData);

            if (!result.Success)
                throw new Exception(result.Message);

            response.Publish("Session name", creationData.Name);
        }
    }
}
