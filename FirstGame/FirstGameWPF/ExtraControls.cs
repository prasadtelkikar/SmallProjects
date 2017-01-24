using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace FirstGameWPF
    {
        class ExtraControls
        {
            public static Line DrawLine (double x1, double y1, double x2, double y2, Color lineColor)
            {
                bool isDashed = false;
                return DrawLine(x1, y1, x2, y2, lineColor, isDashed);
            }

            public static Line DrawLine (double x1, double y1, double x2, double y2, Color lineColor, bool isDashed)
            {
                Line line = new Line();
                line.X1 = x1;
                line.X2 = x2;
                line.Y1 = y1;
                line.Y2 = y2;

                if(isDashed)
                    {
                    DoubleCollection dashCol = new DoubleCollection(2);
                    dashCol.Add(2);
                    dashCol.Add(4);
                    line.StrokeDashArray = dashCol;
                    }

                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = lineColor;
                line.StrokeThickness = 2;
                line.Stroke = brush;

                return line;
            }

            internal static Path AnimateStriker (EllipseGeometry myEllipseGeometry, Line redLine)
                {
                // Create a Path object to contain the geometry.
                Path myPath = new Path();
                myPath.Fill = Brushes.CadetBlue;
                myPath.Stroke = Brushes.Black;
                myPath.StrokeThickness = 1;
                double distance = Math.Sqrt(Math.Pow((redLine.X2 - redLine.X1), 2) + (Math.Pow((redLine.Y2 - redLine.Y1), 2)));
                myPath.Data = myEllipseGeometry;

                // Create a PointAnimation to animate the center of the ellipse.
                PointAnimation myPointAnimation = new PointAnimation();
                myPointAnimation.From = new Point(redLine.X1 + 15, redLine.Y1 + 15);
                myPointAnimation.To = new Point(redLine.X2, redLine.Y2 ) ;
                myPointAnimation.Duration = CarromHelper.GetStrikerSpeed(distance); //new Duration(TimeSpan.FromMilliseconds(5000));
                //myPointAnimation.AutoReverse = true;
                //myPointAnimation.RepeatBehavior = RepeatBehavior.Forever;

                Storyboard.SetTargetName(myPointAnimation, "MyEllipseGeometry");
                Storyboard.SetTargetProperty(myPointAnimation,
                    new PropertyPath(EllipseGeometry.CenterProperty));
                Storyboard myStoryboard = new Storyboard();
                myStoryboard.Children.Add(myPointAnimation);

                // Use an anonymous event handler to begin the animation
                // when the path is loaded.
                myPath.Loaded += delegate(object sender, RoutedEventArgs args)
                {
                    myStoryboard.Begin(myPath);

                };
                return myPath;
                }
        }
    }
