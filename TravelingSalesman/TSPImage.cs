using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TravelingSalesman {
    class TSPImage {
        private static Brush CITY_BRUSH = Brushes.Blue;
        private static Brush PATH_BRUSH = Brushes.Red;
        private static Brush TEXT_BRUSH = Brushes.Black;
        private static Font TEXT_FONT = new Font("Tahoma", 8);
        private static int CITY_RADIUS = 3;

        private Bitmap bitmap;
        private Graphics graphics;

        public TSPImage(int Width, int Height) {
            this.bitmap = new Bitmap(Width, Height);
            this.graphics = Graphics.FromImage(this.bitmap);
            this.graphics.SmoothingMode = SmoothingMode.AntiAlias;
            this.graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            this.graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            this.graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        }

        public void Display() {
            Form form = new Form();
            form.Size = new Size(new Point(this.bitmap.Width + 8, this.bitmap.Height + 30));
            PictureBox pb = new PictureBox();
            pb.Size = new Size(new Point(this.bitmap.Width, this.bitmap.Height));
            pb.Image = this.bitmap;
            form.Controls.Add(pb);
            form.Text = "Traveling Salesman";
            Application.Run(form);
        }

        public void DrawTour(TSPStats stats) {
            TSPTour Tour = (TSPTour)stats.solution;
            TSPCity[] cities = Tour.GetCities();
            int[] tour = Tour.GetTour();
            foreach(TSPCity city in cities) {
                this.DrawCity(city);
            }
            for(int i=0; i<tour.Length-1; i++) {
                TSPCity from = cities[tour[i]];
                TSPCity to = cities[tour[i + 1]];
                this.DrawPath(from, to);
            }
            this.DrawStats(stats);
        }

        private void DrawStats(TSPStats stats) {
            TSPTour Tour = (TSPTour)stats.solution;
            string text = String.Format("Score: {0}\nIterations:{1}\nCities: {2}", Tour.Score(), stats.iterations, Tour.GetCities().Length);
            RectangleF pos = new RectangleF(2, 2, this.bitmap.Width - 2, this.bitmap.Height - 2);
            this.graphics.DrawString(text, TEXT_FONT, TEXT_BRUSH, pos);
        }

        private void DrawCity(TSPCity City) {
            this.graphics.FillEllipse(CITY_BRUSH, City.x - CITY_RADIUS, City.y - CITY_RADIUS, CITY_RADIUS * 2, CITY_RADIUS * 2);
        }

        private void DrawPath(TSPCity From, TSPCity To) {
            this.graphics.DrawLine(new Pen(PATH_BRUSH), From.x, From.y, To.x, To.y);
        }
    }
}
