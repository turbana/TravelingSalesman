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
        public int Settle;          /* iterations after each cooling */
    };

    public struct TSPStats {
        public IAnnealingSolution solution;
        public int iterations;
        public bool working;
        public double heat;
    }

    public interface IAnnealingSolution {
        int Score();
        IAnnealingSolution Neighbor();
    }

    class SimulatedAnnealing {
        public static TSPStats Solve(AnnealingParameters parms, IAnnealingSolution initial, TSPImage image) {
            IAnnealingSolution best, state, neighbor;
            best = initial;
            state = initial;
            Random rand = new Random();
            double heat = parms.Heat;
            int iterations = 0;
            while(heat > parms.MinHeat) {
                for(int i=0; i<parms.Settle; i++) {
                    iterations++;
                    if((iterations % 10000) == 0) {
                        Console.WriteLine("{0,4}: {1}", best.Score(), heat);
                        image.DrawTour(new TSPStats {
                            solution = best,
                            iterations = iterations,
                            working = true,
                            heat = heat,
                        });
                    }
                    neighbor = state.Neighbor();
                    if(neighbor.Score() < state.Score()) {
                        state = neighbor;
                    } else if(rand.NextDouble() < Math.Exp((state.Score() - neighbor.Score()) / heat)) {
                        state = neighbor;
                    }

                    if(state.Score() < best.Score()) {
                        best = state;
                    }
                }
                heat *= parms.Alpha;
            }

            return new TSPStats {
                solution = best,
                iterations = iterations,
                working = false,
                heat = heat,
            };
        }
    }
}
