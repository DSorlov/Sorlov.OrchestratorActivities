using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Text
{
    [Activity]
    public class RegexpCapture
    {
        private String sequence;
        private String pattern;
        private Int16 group;

        [ActivityInput]
        public String Sequence { set { sequence = value; } }

        [ActivityInput]
        public String Pattern { set { pattern = value; } }

        [ActivityInput("Group to Find")]
        public Int16 GroupToFind { set { group = value; } }

        [ActivityOutput(Description = "The nth group.")]
        public String Found
        {
            get
            {
                Regex regex = new Regex(pattern);
                Match match = regex.Match(sequence);
                if (match.Success && group < match.Groups.Count)
                {
                    return match.Groups[group].Value;
                }

                return string.Empty;
            }
        }
    }

}
