using System;
using System.IO;
using System.Linq;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.IO
{
    [ActivityData]
    public class DirectoryInfoData
    {
        private readonly DirectoryInfo info;

        public DirectoryInfoData(DirectoryInfo info)
        {
            this.info = info;
        }

        [ActivityOutput(Description = "The date/time the directory was created")]
        [ActivityFilter]
        public DateTime Created
        {
            get { return info.CreationTime; }
        }

        [ActivityOutput(Description = "The length of the directory and all subdirectories in bytes"), ActivityFilter]
        public long Length
        {
            get { return info.GetFiles("*.*", SearchOption.AllDirectories).Sum(file => file.Length);; }
        }

        [ActivityOutput(Description = "The date/time the directory was created")]
        [ActivityFilter]
        public DateTime Modified
        {
            get { return info.LastWriteTime; }
        }

        [ActivityOutput(Description = "The date/time the directory was accessed"), ActivityFilter]
        public DateTime Accessed
        {
            get { return info.LastAccessTime; }
        }

        [ActivityOutput(Description = "Is the directory read-only"), ActivityFilter]
        public Boolean ReadOnly
        {
            get { return (info.Attributes & System.IO.FileAttributes.ReadOnly) != 0; }
        }

        [ActivityOutput(Description = "The name of the directory"), ActivityFilter]
        public String Name
        {
            get { return info.Name; }
        }
        [ActivityOutput(Description = "The full name of the directory"), ActivityFilter]
        public String FullName
        {
            get { return info.FullName; }
        }

    }

}
