using System.Globalization;
using System.Windows.Media;
using System.Windows;

namespace BinaryTree
{
    internal class Node
    {
        public Node left;
        public Node right;
        public double value;
        public double x;
        public double y;


        public Node(double value)
        {
            this.value = value;
        }

        public Node(double value, double x, double y)
        {
            this.value = value;
            this.x = x;
            this.y = y;
        }

        public void AddNode(Node n)
        {
            int l = MainWindow.rnd.Next(20, 50);

            if (n.value < value)
            {
                if (left == null)
                {
                    
                    left = n;
                    left.x = x - 50;
                    left.y = y + l;

                    MainWindow.list.Add(n.value);
                }
                else
                {
                    left.AddNode(n);
                }
            }
            else if (n.value > value)
            {
                if (right == null)
                {
                    right = n;
                    right.x = x + 50;
                    right.y = y + l;

                    MainWindow.list.Add(n.value);
                }
                else
                {
                    right.AddNode(n);
                }
            }
        }

        public Node Search(double value)
        {
            if (this.value == value)
            {
                return this;
            }
            else if (value < this.value && left != null)
            {
                return left.Search(value);
            }
            else if (value > this.value && right != null)
            {
                return right.Search(value);
            }
            return null;
        }

        public void Visit(Node parent)
        {
            if (left != null)
            {
                left.Visit(this);
            }

            
            var text = new FormattedText(value.ToString(), CultureInfo.GetCultureInfo("en-us"),
                        FlowDirection.LeftToRight, new Typeface("Verdana"), 16, Brushes.LimeGreen,
                        VisualTreeHelper.GetDpi(MainWindow.visual).PixelsPerDip);
            MainWindow.dc.DrawText(text, new Point(x, y));
            var p0 = new Point(parent.x, parent.y);
            var p1 = new Point(x, y);
            MainWindow.dc.DrawLine(new Pen(Brushes.White, 0.5), p0, p1);

            if (right != null)
            {
                right.Visit(this);
            }
        }
    }
}
