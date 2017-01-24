using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FirstGameWPF
    {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        {

        static double striker_x = 0;
        static double striker_y = 0;
        private Line redLine = null;
        private Ellipse strikerWithDefaultLocation = null;
        private Point lastPoint, nextPoint;
        private EllipseGeometry myEllipseGeometry;
        private Label speedIndicator;
        public MainWindow ()
            {
            InitializeComponent();
            strikerWithDefaultLocation = CarromPieces.CreateDefaultStriker();
            CanvasBoard.Children.Add(strikerWithDefaultLocation);
            //Striker --> implement it to move striker


            myEllipseGeometry = new EllipseGeometry();
            myEllipseGeometry.Center = new Point(strikerWithDefaultLocation.Margin.Left, strikerWithDefaultLocation.Margin.Top);
            myEllipseGeometry.RadiusX = 14;
            myEllipseGeometry.RadiusY = 14;
            this.RegisterName("MyEllipseGeometry", myEllipseGeometry);
            }


        private void CanvasBoard_OnMouseLeftButtonDown (object sender, MouseButtonEventArgs e)
            {
            Point p = Mouse.GetPosition(CanvasBoard);
            double x = p.X;
            double y = p.Y;
            //Ellipse piece = CarromPieces.CreatePiece(x, y, Colors.Red, Colors.Black);
            //CanvasBoard.Children.Add(piece);
           
            }

        private void Window_KeyDown (object sender, KeyEventArgs e)
            {
            if ( e.Key == Key.Left )
                {
                if ( LeftRedCircleOnBottomBar.Margin.Left <= strikerWithDefaultLocation.Margin.Left - 5 )
                    {
                    strikerWithDefaultLocation.Margin = new Thickness()
                    {
                        Left = strikerWithDefaultLocation.Margin.Left - 2,
                        Top = strikerWithDefaultLocation.Margin.Top,
                        Right = strikerWithDefaultLocation.Margin.Right,
                        Bottom = strikerWithDefaultLocation.Margin.Bottom
                    };
                    }
                }
            else if ( e.Key == Key.Right )
                {
                if ( strikerWithDefaultLocation.Margin.Left >= RightRedCircleOnBottomBar.Margin.Left - 5 )
                    {
                    strikerWithDefaultLocation.Margin = new Thickness()
                    {
                        Left = strikerWithDefaultLocation.Margin.Left + 2,
                        Top = strikerWithDefaultLocation.Margin.Top,
                        Right = strikerWithDefaultLocation.Margin.Right,
                        Bottom = strikerWithDefaultLocation.Margin.Bottom
                    };
                    }
                }
            }

        private void CanvasBoard_OnMouseMove (object sender, MouseEventArgs e)
            {

            lastPoint = Mouse.GetPosition(CanvasBoard);

            if ( redLine != null && redLine.IsEnabled )
                CanvasBoard.Children.Remove(redLine);
            

            redLine = ExtraControls.DrawLine(strikerWithDefaultLocation.Margin.Left + 15,
                strikerWithDefaultLocation.Margin.Top + 15, lastPoint.X, lastPoint.Y, Colors.Red, true);

            CanvasBoard.Children.Add(redLine);


            //if ( speedIndicator != null )
            //    CanvasBoard.Children.Remove(speedIndicator);
            //speedIndicator = new Label();
            //speedIndicator.FontSize = 15;
            //speedIndicator.FontWeight = FontWeights.Medium;
            //speedIndicator.Content = CarromHelper.GetSpeedIndicator(redLine);
            //speedIndicator.Margin = new Thickness(lastPoint.X, lastPoint.Y, 0, 0);

            //CanvasBoard.Children.Add(speedIndicator);
            }

        private void GetNextPoint (double x1, double y1, double x2, double y2)
            {
            // New way adding refresh
            double m = ((y2 - y1) / (x2 - x1));
            double c = (y1 - (x1 * m));
            double moveX = x1;
            double moveY = y1;

            if ( x2 > moveX )
                {
                while ( moveX < x2 )
                    {
                    moveX = moveX + 1;
                    moveY = ((m * moveX) + c);

                    Ellipse nextMove = CarromPieces.CreatePiece(moveX, moveY, Colors.CadetBlue, Colors.Black);

                    CanvasBoard.Children.Add(nextMove);

                    if ( moveX > x2 )
                        break;
                    }
                }
            else if ( x2 < moveX )
                {
                while ( x2 < moveX )
                    {
                    moveX = moveX - 1;
                    moveY = ((m * moveX) + c);

                    Ellipse nextMove = CarromPieces.CreatePiece(moveX, moveY, Colors.CadetBlue, Colors.Black);

                    CanvasBoard.Children.Add(nextMove);

                    if ( x2 > moveX )
                        break;
                    }
                }
            }

        private void CanvasBoard_OnMouseLeftButtonUp (object sender, MouseButtonEventArgs e)
            {
            //GetNextPoint(strikerWithDefaultLocation.Margin.Left,
            //     strikerWithDefaultLocation.Margin.Top, lastPoint.X, lastPoint.Y);

            if ( myEllipseGeometry != null && redLine != null )
                {
                CanvasBoard.Children.Remove(strikerWithDefaultLocation);
                CanvasBoard.Children.Remove(redLine);
                Path animatedPath = ExtraControls.AnimateStriker(myEllipseGeometry, redLine);
                CanvasBoard.Children.Add(animatedPath);
                }

            }
        }
    }