using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesman {
    struct TSPCity {
        public int x;
        public int y;
    }

    class TSPTour {
        private TSPCity[] cities;
        private int[] tour;

        public TSPTour(TSPCity[] Cities, int[] Tour) {
            this.cities = Cities;
            this.tour = Tour;
        }

        public TSPCity[] GetCities() {
            return this.cities;
        }

        public int[] GetTour() {
            return this.tour;
        }

        public static TSPCity[] RandomCities(int Count, int MaxX, int MaxY) {
            TSPCity[] cities = new TSPCity[Count];
            Random rand = new Random();
            for(int i=0; i<Count; i++) {
                TSPCity city = new TSPCity();
                city.x = rand.Next(MaxX);
                city.y = rand.Next(MaxY);
                cities[i] = city;
            }
            return cities;
        }

        public static TSPTour RandomTour(TSPCity[] Cities) {
            int[] tour = new int[Cities.Length];
            Random rand = new Random();
            int i;
            for(i=0; i<tour.Length; i++) {
                tour[i] = -1;
            }
            for(int city=0; city<Cities.Length; city++) {
                do {
                    i = rand.Next(tour.Length);
                } while (tour[i] != -1);
                tour[i] = city;
            }
            return new TSPTour(Cities, tour);
        }
    }
}
