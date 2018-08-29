using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.IO
{
    [Activity("File Size")]
    public class FileSize
    {
        private System.IO.FileInfo fileInfo;

        [ActivityInput("File Path")]
        public System.IO.FileInfo FilePath
        {
            set { fileInfo = value; }
        }

        [ActivityOutput(Description = "Length in bytes")]
        public Int64 LengthInBytes
        {
            get
            {
                if (fileInfo.Exists)
                {
                    return fileInfo.Length;
                }

                throw new ApplicationException("File not found");
            }
        }
        [ActivityOutput(Description = "Length in bytes")]
        public Int64 LengthInMegabytes
        {
            get
            {
                if (fileInfo.Exists)
                {
                    return fileInfo.Length / 1024;
                }

                throw new ApplicationException("File not found");
            }
        }
        [ActivityOutput(Description = "Length in bytes")]
        public Int64 LengthInGigabytes
        {
            get
            {
                if (fileInfo.Exists)
                {
                    return fileInfo.Length / 1024 / 1024;
                }

                throw new ApplicationException("File not found");
            }
        }
    }

}
