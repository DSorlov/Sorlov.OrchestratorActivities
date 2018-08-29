using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Mail
{
    [ActivityData("SMTP Mail Settings")]
    public class SmtpMailSettings
    {
        private String host = string.Empty;
        private String userName = string.Empty;
        private String password = string.Empty;

        [ActivityInput, ActivityOutput]
        public String MailServer
        {
            get { return host; }
            set { host = value; }
        }

        [ActivityInput]
        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [ActivityInput(PasswordProtected = true)]
        public String Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
