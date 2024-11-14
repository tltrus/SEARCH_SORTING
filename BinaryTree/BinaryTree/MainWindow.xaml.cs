using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;


namespace BinaryTree
{
    /// <summary>
    /// Coding Challenge #65 - Binary Tree
    /// https://thecodingtrain.com/CodingChallenges/065.2-binary-tree-viz.html
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer;
        public static List<double> list;
        public static Random rnd = new Random();

        public static DrawingVisual visual;
        public static DrawingContext dc;
        public static double width, height;
        Tree tree;
        int counter;

        public MainWindow()
        {
            InitializeComponent();

            width = g.Width;
            height = g.Height;

            visual = new DrawingVisual();
            tree = new Tree();
            list = new List<double>();

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
        }

        private void timerTick(object sender, EventArgs e) => Drawing();

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            tree = new Tree();
            list = new List<double>();
            counter = 0;
            timer.Start();
        }

        private void Drawing()
        {
            if (counter >= 15)
            {
                timer.Stop();
                return;
            }

            g.RemoveVisual(visual);

            using (dc = visual.RenderOpen())
            {
                var n = rnd.Next(1, 50);
                tree.AddValue(n);
                tree.Traverse();

                DrawNumbersList(dc);

                counter++;

                dc.Close();
                g.AddVisual(visual);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            double num = 0;

            try
            {
                num = double.Parse(tbSearch.Text);

                var founded = tree.Search(num);

                g.RemoveVisual(visual);
                using (dc = visual.RenderOpen())
                {
                    tree.Traverse();
                    DrawNumbersList(dc);

                    DrawText(dc, founded.value.ToString(), 16, Brushes.Red, founded.x, founded.y);

                    dc.Close();
                    g.AddVisual(visual);
                }
            }
            catch
            {
                return;
            }
        }

        private void DrawNumbersList(DrawingContext dc)
        {
            for (int i = 0; i < list.Count; i++)
            {
                double x = 40 * i + 5;
                double y = height - 50;
                DrawText(dc, list[i].ToString(), 12, Brushes.White, x, y);
            }
        }

        private void DrawText(DrawingContext dc, string text, double textSize, Brush brush, double x, double y)
        {
            var formattedText = new FormattedText(text, CultureInfo.GetCultureInfo("en-us"),
                                        FlowDirection.LeftToRight, new Typeface("Verdana"), textSize, brush,
                                        VisualTreeHelper.GetDpi(visual).PixelsPerDip);
            dc.DrawText(formattedText, new Point(x, y));
        }
    }
}
