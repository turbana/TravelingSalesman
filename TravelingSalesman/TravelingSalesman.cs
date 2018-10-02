using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesman {
    struct TSPCity {
        public int x;
        public int y;

        public double Distance(TSPCity other) {
            return Math.Sqrt(
                Math.Pow(this.x - other.x, 2) +
                Math.Pow(this.y - other.y, 2));
        }
    }

    class TSPTour : IAnnealingSolution {
        private TSPCity[] cities;
        private int[] tour;
        private int score;

        public TSPTour(TSPCity[] Cities, int[] Tour) {
            this.cities = Cities;
            this.tour = new int[Tour.Length];
            Array.Copy(Tour, this.tour, this.tour.Length);
            this.score = -1;
        }

        public TSPCity[] GetCities() {
            return this.cities;
        }

        public int[] GetTour() {
            return this.tour;
        }

        public int Score() {
            if(this.score >= 0) {
                return this.score;
            }
            double score = 0.0;
            TSPCity from, to;
            for(int i=0; i<this.tour.Length; i++) {
                from = this.cities[this.tour[i]];
                to = this.cities[this.tour[(i + 1) % this.tour.Length]];
                score += from.Distance(to);
            }
            this.score = (int)score;
            return this.score;
        }

        public IAnnealingSolution Neighbor() {
            Random rand = new Random();
            TSPTour other = new TSPTour(this.cities, this.tour);
            
            int x, y;
            do {
                x = rand.Next(this.tour.Length);
                y = rand.Next(this.tour.Length);
            } while (x == y);
            other.tour[x] = this.tour[y];
            other.tour[y] = this.tour[x];
            return other;
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
