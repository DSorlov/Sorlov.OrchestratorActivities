using System.Runtime.Serialization;

namespace Sorlov.OrchestratorActivities.PSRunspaceManager.Dto
{
    [DataContract]
    public class RemoteOperationResult
    {
        [DataMember] public bool Success;
        [DataMember] public string Message;       
    }
}
