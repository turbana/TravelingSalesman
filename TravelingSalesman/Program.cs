using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelingSalesman {
    static class Program {
        private const int CITIES_COUNT = 15;
        private const int IMAGE_WIDTH = 512;
        private const int IMAGE_HEIGHT = 512;
        private static AnnealingParameters ANNEALING_PARAMETERS = new AnnealingParameters() {
            Heat = 1.0,
            Alpha = 0.99,
            MinHeat = 0.01,
            Settle = 1000,
        };

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            TSPCity[] cities = TSPTour.RandomCities(CITIES_COUNT, IMAGE_WIDTH, IMAGE_HEIGHT);
            TSPTour initial = TSPTour.RandomTour(cities);
            TSPTour best = (TSPTour)SimulatedAnnealing.Solve(ANNEALING_PARAMETERS, initial);
            Console.WriteLine("---");
            for(int i=0; i<CITIES_COUNT; i++) {
                Console.Write("{0} ", best.GetTour()[i]);
            }
            Console.WriteLine();
            TSPImage image = new TSPImage(IMAGE_WIDTH, IMAGE_HEIGHT);
            image.DrawTour(best);
            image.Display();
        }
    }
}
