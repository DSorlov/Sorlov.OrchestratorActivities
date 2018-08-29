using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.IO
{
    [ActivityData]
    public class ACLEntry
    {
        private readonly FileSystemAccessRule info;

        public ACLEntry(FileSystemAccessRule info)
        {
            this.info = info;
        }


        [ActivityOutput(Description = "User this conforms to")]
        [ActivityFilter]
        public string User
        {
            get { return info.IdentityReference.ToString(); }
        }

        [ActivityOutput(Description = "Rights given")]
        [ActivityFilter]
        public string Rights
        {
            get { return info.FileSystemRights.ToString(); }
        }

    }

}
