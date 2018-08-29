using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Net
{
    [Activity]
    public class PingHost
    {
        private string hostNameOrAddress;
        private int timeout = 120;

        [ActivityInput("Hostname/Address")]
        public string HostNameOrAddress
        {
            set { hostNameOrAddress = value; }
        }

        [ActivityInput(Optional = true)]
        public int Timeout
        {
            set { timeout = value; }
        }

        [ActivityOutput]
        public PingReplyData Reply
        {
            get
            {
                using (var pingSender = new Ping())
                {
                    return new PingReplyData(pingSender.Send(hostNameOrAddress, timeout));
                }
            }
        }
    }

}
