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
        private static Brush CLEAR_BRUSH = Brushes.White;
        private static Font TEXT_FONT = new Font("Tahoma", 8);
        private static int CITY_RADIUS = 3;

        private Bitmap bitmap;
        private PictureBox pb;
        private Form form;
        private readonly object Lock = new object();

        public TSPImage(int Width, int Height) {
            this.bitmap = new Bitmap(Width, Height);
            Form form = new Form();
            form.Size = new Size(new Point(Width + 8, Height + 30));
            PictureBox pb = new PictureBox();
            pb.Size = new Size(new Point(Width, Height));
            pb.Image = this.bitmap;
            form.Controls.Add(pb);
            form.Text = "Traveling Salesman";
            this.pb = pb;
            this.form = form;
        }

        public void Display() {
            Application.Run(this.form);
        }

        private Graphics GetGraphics() {
            Graphics graphics = Graphics.FromImage(this.bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            return graphics;
        }

        private void Clear(Graphics g) {
            g.FillRectangle(CLEAR_BRUSH, 0, 0, this.bitmap.Width, this.bitmap.Height);
        }

        public void DrawTour(TSPStats stats) {
            lock (Lock) {
                Graphics g = this.GetGraphics();
                this.Clear(g);
                TSPTour Tour = (TSPTour)stats.solution;
                TSPCity[] cities = Tour.GetCities();
                int[] tour = Tour.GetTour();
                foreach (TSPCity city in cities) {
                    this.DrawCity(g, city);
                }
                for (int i = 0; i < tour.Length - 1; i++) {
                    TSPCity from = cities[tour[i]];
                    TSPCity to = cities[tour[i + 1]];
                    this.DrawPath(g, from, to);
                }
                this.DrawStats(g, stats);
                g.Flush();
                g.Dispose();
            }
            // refresh display
            this.form.Invoke(new Action(() => {
                lock (Lock) {
                    this.pb.Image = this.bitmap;
                }
            }));
        }

        private void DrawStats(Graphics g, TSPStats stats) {
            TSPTour Tour = (TSPTour)stats.solution;
            string text = "";
            text += String.Format("Score: {0}\n", Tour.Score());
            text += String.Format("Iterations: {0}\n", stats.iterations);
            text += String.Format("Cities: {0}\n", Tour.GetCities().Length);
            if(stats.working) {
                text += String.Format("Heat: {0}\n", stats.heat);
                text += "Searching...";
            }
            RectangleF pos = new RectangleF(2, 2, this.bitmap.Width - 2, this.bitmap.Height - 2);
            g.DrawString(text, TEXT_FONT, TEXT_BRUSH, pos);
        }

        private void DrawCity(Graphics g, TSPCity City) {
            g.FillEllipse(CITY_BRUSH, City.x - CITY_RADIUS, City.y - CITY_RADIUS, CITY_RADIUS * 2, CITY_RADIUS * 2);
        }

        private void DrawPath(Graphics g, TSPCity From, TSPCity To) {
            g.DrawLine(new Pen(PATH_BRUSH), From.x, From.y, To.x, To.y);
        }
    }
}
