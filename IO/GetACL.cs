using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.IO
{
    [Activity]
    public class GetACL : IActivity
    {
        private readonly static string PATH = "Path";

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput(PATH).WithFileBrowser();
            designer.AddCorellatedData(typeof(ACLEntry));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            List<ACLEntry> aclEntries = new List<ACLEntry>();
            FileAttributes attr = File.GetAttributes(request.Inputs[PATH].AsString());
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(request.Inputs[PATH].AsString());
                foreach (FileSystemAccessRule fsAccessRule in dirInfo.GetAccessControl().GetAccessRules(true,true,typeof(System.Security.Principal.NTAccount)))
                {
                    aclEntries.Add(new ACLEntry(fsAccessRule));
                }
            }
            else
            {
                FileInfo fileInfo = new FileInfo(request.Inputs[PATH].AsString());
                foreach (FileSystemAccessRule fsAccessRule in fileInfo.GetAccessControl().GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount)))
                {
                    aclEntries.Add(new ACLEntry(fsAccessRule));
                }
            }
            response.PublishRange(aclEntries);
        }

    }

}
