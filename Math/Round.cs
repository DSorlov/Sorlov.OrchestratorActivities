using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Math
{
    [Activity("Round")]
    public class Round
    {
        private MidpointRounding mode;
        private int decimals;
        private Decimal value;

        [ActivityInput("Mode"), ActivityOutput("Mode")]
        public MidpointRounding Algorithm
        {
            get { return mode; }
            set { mode = value; }
        }

        [ActivityInput, ActivityOutput]
        public int Decimals
        {
            get { return decimals; }
            set { decimals = value; }
        }

        [ActivityInput, ActivityOutput]
        public Decimal Value
        {
            get { return value; }
            set { this.value = value; }
        }

        [ActivityOutput("Rounded Value")]
        public Decimal RoundedValue
        {
            get { return Decimal.Round(value, decimals, mode); }
        }
    }

}
