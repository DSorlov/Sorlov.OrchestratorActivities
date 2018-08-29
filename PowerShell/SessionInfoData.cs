using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.PowerShell
{
    [ActivityData]
    public class SessionInfoData
    {
        private readonly string key;
        private readonly string status;

        internal SessionInfoData(string key, string status)
        {
            this.key = key;
            this.status = status;
        }

        [ActivityOutput("Session name")]
        public string Key
        {
            get { return key; }
        }

        [ActivityOutput("Session status")]
        public string Status
        {
            get { return status; }
        }
    }

}
