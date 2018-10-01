using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesman {
    public struct AnnealingParameters {
        public double Heat;         /* initial heat value */
        public double Alpha;        /* cooling rate */
        public double MinHeat;      /* minimum heat */
    };

    interface IAnnealingSolution {
        int Score();
        IAnnealingSolution Neighbor();
    }

    class SimulatedAnnealing {
        public static IAnnealingSolution Solve(AnnealingParameters parms, IAnnealingSolution initial) {
            return null;
        }
    }
}
