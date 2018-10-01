using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace TravelingSalesman {
    static class Program {
        private const int CITIES_COUNT = 32;
        private const int IMAGE_WIDTH = 512;
        private const int IMAGE_HEIGHT = 512;
        private static AnnealingParameters ANNEALING_PARAMETERS = new AnnealingParameters() {
            Heat = 1.0,
            Alpha = 0.99,
            MinHeat = 0.01,
            Settle = 5000,
        };

        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            TSPImage image = new TSPImage(IMAGE_WIDTH, IMAGE_HEIGHT);
            Thread thread = new Thread(() => Solve(image));
            thread.Start();
            image.Display();
        }

        static void Solve(TSPImage image) {
            TSPCity[] cities = TSPTour.RandomCities(CITIES_COUNT, IMAGE_WIDTH, IMAGE_HEIGHT);
            TSPTour initial = TSPTour.RandomTour(cities);
            TSPStats results;
            try {
                results = SimulatedAnnealing.Solve(ANNEALING_PARAMETERS, initial, image);
                image.DrawTour(results);
            } catch (ObjectDisposedException e) {
                // ignore window closed
            }
        }
    }
}
