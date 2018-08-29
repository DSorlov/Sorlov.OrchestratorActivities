using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.IO
{
    [Activity("Rename path")]
    public class RenamePath
    {
        private String oldName = "";
        private String newName = "";

        [ActivityInput("Old Name")]
        public String OldName
        {
            set { oldName = value; }
        }

        [ActivityInput("New Name")]
        public String NewName
        {
            set { newName = value; }
        }

        [ActivityMethod]
        public void Invoke()
        {
            FileAttributes attr = File.GetAttributes(oldName);

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                Directory.Move(oldName,newName);
            else
                File.Move(oldName,newName);
        }
    }

}
