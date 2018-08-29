using System;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Text
{
    [Activity]
    public class TextSplit
    {
        private string text;
        private string separators;
        private StringSplitOptions options;

        [ActivityInput]
        public string Text
        {
            set { text = value; }
        }

        [ActivityInput]
        public string Separators
        {
            set { separators = value; }
        }

        [ActivityInput("Remove Empty Entries", Default = true)]
        public bool RemoveEmpty
        {
            set { options = value ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None; }
        }

        [ActivityOutput, ActivityFilter]
        public string[] Items
        {
            get { return text.Split(separators.ToCharArray(), options); }
        }
    }

}
