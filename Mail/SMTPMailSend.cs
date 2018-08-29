using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Mail
{
    [Activity("SMTP Send Mail")]
    public class SmtpMailSend
    {
        private SmtpMailSettings settings;
        private MailAddress to;
        private MailAddress from;
        private string subject;
        private string body;

        [ActivityConfiguration(Usage = ConfigurationUsage.ExecuteOnly), ActivityOutput]
        public SmtpMailSettings Settings
        {
            set { settings = value; }
            get { return settings; }
        }

        [ActivityInput]
        public MailAddress To
        {
            set { to = value; }
        }

        [ActivityInput]
        public MailAddress From
        {
            set { from = value; }
        }

        [ActivityInput]
        public string Subject
        {
            set { subject = value; }
        }

        [ActivityInput]
        public string Body
        {
            set { body = value; }
        }

        [ActivityMethod]
        public void Send()
        {
            using (var email = new MailMessage(from, to))
            {
                email.Subject = subject;
                email.Body = body;

                SmtpClient smtpClient = new SmtpClient(settings.MailServer);
                smtpClient.Credentials = new NetworkCredential(settings.UserName, settings.Password);

                smtpClient.Send(email);
            }
        }
    }

}
