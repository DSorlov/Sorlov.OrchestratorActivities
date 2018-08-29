using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Sorlov.OrchestratorActivities.Math
{
    [Activity("RandomNumber")]
    public class RandomNumber
    {
        private UInt16 howMany;
        private Int32 seed;

        [ActivityInput]
        public UInt16 HowMany
        {
            set { howMany = value; }
        }

        [ActivityInput]
        public Int32 Seed
        {
            set { seed = value; }
        }

        [ActivityOutput]
        public IEnumerable<Int32> Result
        {
            get
            {
                Random random = new Random(seed);
                for (UInt16 n = 0; n < howMany; ++n)
                {
                    yield return random.Next();
                }
            }
        }
    }

}
