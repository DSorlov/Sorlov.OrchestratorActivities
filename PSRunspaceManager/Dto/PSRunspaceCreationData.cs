using System.Management.Automation;
using System.Runtime.Serialization;

namespace Sorlov.OrchestratorActivities.PSRunspaceManager.Dto
{
    [DataContract]
    public class PSRunspaceCreationData
    {
        [DataMember] public string Name = "";

        [DataMember] public string Hostname = "localhost";

        [DataMember] public int Port = 5985;

        [DataMember] public PSCredential Credentials;

        [DataMember] public bool UseSsl = false;

        [DataMember] public bool LoadMachineProfile = true;
    }
}
