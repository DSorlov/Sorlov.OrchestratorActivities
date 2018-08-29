using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.IO
{
    [Activity]
    public class DirectoryList : IActivity
    {
        private readonly static string PATH = "Path";
        private readonly static string PATTERN = "Pattern";
        private readonly static string NUM_FILES = "Number of Directories";

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput(PATH).WithFileBrowser();
            designer.AddInput(PATTERN).WithDefaultValue("*");
            designer.AddOutput(NUM_FILES).AsNumber();
            designer.AddCorellatedData(typeof(DirectoryInfoData));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            DirectoryInfo path = request.Inputs[PATH].As<DirectoryInfo>();
            string pattern = request.Inputs[PATTERN].AsString();
            IEnumerable files = FindFiles(path, pattern);
            int numFiles = response.WithFiltering().PublishRange(files);
            response.Publish(NUM_FILES, numFiles);
        }

        private IEnumerable<DirectoryInfoData> FindFiles(DirectoryInfo path, string pattern)
        {
            return path.GetDirectories(pattern).Select(info => new DirectoryInfoData(info));
        }
    }

}
