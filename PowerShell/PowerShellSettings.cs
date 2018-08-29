using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.PowerShell
{
    [ActivityData("PowerShell Authentication Settings")]
    public class PowerShellSettings
    {
        [ActivityInput("User name (RunAs)", Optional = true)]
        public string UserName
        {
            set;
            get;
        }

        [ActivityInput("Password (RunAs)", Optional = true, PasswordProtected = true)]
        public string Password
        {
            set;
            get;
        }
    }

}
