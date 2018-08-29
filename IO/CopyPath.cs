using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.IO
{
    [Activity("Copy path")]
    public class CopyPath
    {
        private string source;
        private string destination;
        private bool overwrite;

        [ActivityInput, ActivityOutput]
        public string Source
        {
            set { source = value; }
            get { return source; }
        }

        [ActivityInput, ActivityOutput]
        public string Destination
        {
            set { destination = value; }
            get { return destination; }
        }

        [ActivityInput(Default = false)]
        public Boolean Overwrite
        {
            set { overwrite = value; }
        }

        [ActivityMethod]
        public void Run()
        {
            FileAttributes attr = File.GetAttributes(source);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory( source, destination, overwrite );
            }
            else
            {
                FileInfo sourceFile = new FileInfo(source);
                sourceFile.CopyTo(destination, overwrite);
            }
        }
    }

}
