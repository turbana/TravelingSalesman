using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelingSalesman {
    static class Program {
        private static int CITIES_COUNT = 15;
        private static int IMAGE_WIDTH = 512;
        private static int IMAGE_HEIGHT = 512;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            TSPTour tour = TSPTour.RandomTour(TSPTour.RandomCities(CITIES_COUNT, IMAGE_WIDTH, IMAGE_HEIGHT));
            foreach(TSPCity city in tour.GetCities()) {
                Console.WriteLine("({0}, {1})", city.x, city.y);
            }
            foreach(int city in tour.GetTour()) {
                Console.Write("{0} ", city);
            }
            Console.WriteLine();
            Console.ReadLine();
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/
        }
    }
}
