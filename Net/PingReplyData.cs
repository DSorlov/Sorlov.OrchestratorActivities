using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Net
{
    [ActivityData]
    public class PingReplyData
    {
        private readonly PingReply reply;

        internal PingReplyData(PingReply reply)
        {
            this.reply = reply;
        }

        [ActivityOutput]
        public IPAddress Address
        {
            get { return reply.Address; }
        }

        [ActivityOutput("Round Trip Time")]
        public long RoundTripTime
        {
            get { return reply.RoundtripTime; }
        }
    }

}
