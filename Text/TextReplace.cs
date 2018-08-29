using System.Text.RegularExpressions;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Text
{
    [Activity]
    class TextReplace
    {
        private string input;
        private string replacement;
        private string pattern;
        private int maxReplace = -1;
        private int startAt = 0;

        [ActivityInput]
        public string Input
        {
            set { input = value; }
        }

        [ActivityInput("Maximum Replacements", Optional = true)]
        public int MaxReplace
        {
            set { maxReplace = value; }
        }

        [ActivityInput("Start At", Optional = true)]
        public int StartAt
        {
            set { startAt = value; }
        }

        [ActivityInput]
        public string Replacement
        {
            set { replacement = value; }
        }

        [ActivityInput]
        public string Pattern
        {
            set { pattern = value; }
        }

        [ActivityOutput("New String")]
        public string NewString
        {
            get
            {
                Regex regex = new Regex(pattern);
                return regex.Replace(input, replacement, maxReplace, startAt);
            }
        }
    }

}
