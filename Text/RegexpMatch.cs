using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Text
{
    [Activity]
    public class RegexpMatch
    {
        private String sequence;
        private String pattern;

        [ActivityInput]
        public String Sequence { set { sequence = value; } }

        [ActivityInput]
        public String Pattern { set { pattern = value; } }

        [ActivityOutput("Matches", Description = "Does the sequence match the pattern.")]
        public Boolean IsMatch
        {
            get
            {
                Regex regex = new Regex(pattern);
                return regex.Match(sequence).Success;
            }
        }
    }

}
