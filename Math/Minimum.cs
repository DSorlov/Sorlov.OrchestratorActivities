using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Math
{
    [Activity("GetMinimum")]
    public class Minimum
    {
        private Double val1 = 0.0;
        private Double val2 = 0.0;

        [ActivityInput("Value 1")]
        public Double Val1
        {
            set { val1 = value; }
        }

        [ActivityInput("Value 2")]
        public Double Val2
        {
            set { val2 = value; }
        }

        [ActivityOutput(Description = "The minimum of two numbers.")]
        public Double Result
        {
            get { return System.Math.Min(val1, val2); }
        }
    }

}
