using System;
using System.IO;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.IO
{
    [ActivityData]
    public class FileInfoData
    {
        private readonly FileInfo info;

        public FileInfoData(FileInfo info)
        {
            this.info = info;
        }

        [ActivityOutput(Description = "The date/time the file was created")]
        [ActivityFilter]
        public DateTime Created
        {
            get { return info.CreationTime; }
        }

        [ActivityOutput(Description = "The length of the file in bytes"), ActivityFilter]
        public long Length
        {
            get { return info.Length; }
        }

        [ActivityOutput(Description = "The date/time the file was created")]
        [ActivityFilter]
        public DateTime Modified
        {
            get { return info.LastWriteTime; }
        }

        [ActivityOutput(Description = "The date/time the file was accessed"), ActivityFilter]
        public DateTime Accessed
        {
            get { return info.LastAccessTime; }
        }

        [ActivityOutput(Description = "Is the file read-only"), ActivityFilter]
        public Boolean ReadOnly
        {
            get { return (info.Attributes & System.IO.FileAttributes.ReadOnly) != 0; }
        }

        [ActivityOutput(Description = "The name of the file"), ActivityFilter]
        public String Name
        {
            get { return info.Name; }
        }
        [ActivityOutput(Description = "The full name of the file"), ActivityFilter]
        public String FullName
        {
            get { return info.FullName; }
        }

        [ActivityOutput(Description = "The extension of the file"), ActivityFilter]
        public String Extension
        {
            get { return Path.GetExtension(info.FullName); }
        }

        [ActivityOutput(Description = "The directory of the file"), ActivityFilter]
        public String Directory
        {
            get { return Path.GetFullPath(info.FullName); }
        }


    }

}
