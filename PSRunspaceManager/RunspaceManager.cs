using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.ServiceModel;
using System.Text;
using Sorlov.OrchestratorActivities.PSRunspaceManager.Dto;
using Sorlov.OrchestratorActivities.PSRunspaceManager.Interfaces;

namespace Sorlov.OrchestratorActivities.PSRunspaceManager
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RunspaceManager : IRunspaceManager
    {
        public static Dictionary<string,Runspace> Runspaces = new Dictionary<string, Runspace>();

        public Dictionary<string, string> GetRunspaces()
        {
            Dictionary<string,string> result = new Dictionary<string, string>();
            foreach (string runspaceName in Runspaces.Keys)
            {
                result.Add(runspaceName,Runspaces[runspaceName].RunspaceAvailability.ToString());   
            }
            return result;
        }

        public RemoteOperationResult ExecuteCommand(string runspaceName, string script)
        {
            if (!Runspaces.ContainsKey(runspaceName.ToUpper()))
                return new RemoteOperationResult() { Message = "Runspace do not exist.", Success = false };

            try
            {
                Runspace currentRunspace = Runspaces[runspaceName.ToUpper()];

                StringBuilder resultText = new StringBuilder();
                using (Pipeline runspace = currentRunspace.CreatePipeline())
                {
                    runspace.Commands.AddScript(script);
                    runspace.Commands.Add(new Command("Out-String"));
                    Collection<PSObject> result = runspace.Invoke();
                    
                    foreach (PSObject resultItem in result)
                    {
                        resultText.AppendLine(resultItem.ToString());
                    }
                }
                return new RemoteOperationResult() { Message = resultText.ToString(), Success = true };
            }
            catch (Exception ex)
            {
                return new RemoteOperationResult() { Message = string.Format("Execute error: {0}", ex.Message), Success = false };
            }
        }

        public RemoteOperationResult OpenRunspace(PSRunspaceCreationData createOptions)
        {
            RemoteOperationResult result = new RemoteOperationResult();

            if (Runspaces.ContainsKey(createOptions.Name.ToUpper()))
                return new RemoteOperationResult() {Message = "Runspace already existed.", Success = true};

            try
            {
                WSManConnectionInfo connectioInfo = new WSManConnectionInfo(createOptions.UseSsl, createOptions.Hostname, createOptions.Port, "/wsman", "http://schemas.microsoft.com/powershell/Microsoft.PowerShell", createOptions.Credentials);
                connectioInfo.NoMachineProfile = !createOptions.LoadMachineProfile;
                connectioInfo.AuthenticationMechanism = AuthenticationMechanism.Negotiate;

                Runspace runspace = RunspaceFactory.CreateRunspace(connectioInfo);
                runspace.Open();
                Runspaces.Add(createOptions.Name.ToUpper(), runspace);
                return new RemoteOperationResult() {Message = "Runspace created.", Success = true};
            }
            catch (Exception ex)
            {
                return new RemoteOperationResult() {Message = string.Format("Runspace creation failed: {0}", ex.Message), Success = false};
            }
        }


        public RemoteOperationResult CloseRunspace(string name)
        {
            if (!Runspaces.ContainsKey(name.ToUpper()))
                return new RemoteOperationResult() {Message = "Runspace did not exist.", Success = true};

            try
            {
                Runspace currentRunspace = Runspaces[name.ToUpper()];
                currentRunspace.Close();

                Runspaces.Remove(name.ToUpper());

                return new RemoteOperationResult() { Message = "Runspace removed.", Success = true };
            }
            catch (Exception ex)
            {
                return new RemoteOperationResult() { Message = string.Format("Runspace removal failed: {0}", ex.Message), Success = false };
            }
        }
    }
}
