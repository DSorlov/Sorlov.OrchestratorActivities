using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Net
{
    [Activity("FTP Download Request")]
    public class FTPDownloadFIle
    {
        private string requestUri;
        private string targetFolder;
        private string username;
        private string password;

        [ActivityInput("File To Download")]
        public string RequestUri
        {
            set { requestUri = value; }
        }

        [ActivityInput("Target Folder")]
        public string TargetFolder
        {
            set { targetFolder = value; }
        }

        [ActivityInput]
        public string Username
        {
            set { username = value; }
        }

        [ActivityInput(PasswordProtected = true)]
        public string Password
        {
            set { password = value; }
        }

        [ActivityMethod]
        public void Run()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(requestUri);
            request.Credentials = new NetworkCredential(username, password);

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Stream data = response.GetResponseStream();
                string targetPath = Path.Combine(targetFolder, Path.GetFileName(requestUri));

                if (System.IO.File.Exists(targetPath))
                {
                    System.IO.File.Delete(targetPath);
                }

                byte[] buffer = new byte[4096];
                using (FileStream output = new FileStream(targetPath, FileMode.CreateNew))
                {
                    int bytesRead = 0;
                    do
                    {
                        bytesRead = data.Read(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            output.Write(buffer, 0, bytesRead);
                        }
                    } while (bytesRead > 0);
                }
            }
        }
    }
}
