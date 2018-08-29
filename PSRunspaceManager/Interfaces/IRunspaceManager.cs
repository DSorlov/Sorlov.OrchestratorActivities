using System.Collections.Generic;
using System.ServiceModel;
using Sorlov.OrchestratorActivities.PSRunspaceManager.Dto;

namespace Sorlov.OrchestratorActivities.PSRunspaceManager.Interfaces
{
    [ServiceContract(Namespace = "http://daniel.sorlov.com/runspacemanager")]
    public interface IRunspaceManager
    {
        [OperationContract]
        RemoteOperationResult OpenRunspace(PSRunspaceCreationData createOptions);

        [OperationContract]
        RemoteOperationResult CloseRunspace(string name);

        [OperationContract]
        RemoteOperationResult ExecuteCommand(string runspaceName, string script);

        [OperationContract]
        Dictionary<string, string> GetRunspaces();

    }


}
