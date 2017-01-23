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
        public MainWindow ()
            {
            InitializeComponent();
            strikerWithDefaultLocation = CarromPieces.CreateDefaultStriker();
            CanvasBoard.Children.Add(strikerWithDefaultLocation);
            //Striker --> implement it to move striker

            }


        private void CanvasBoard_OnMouseLeftButtonDown (object sender, MouseButtonEventArgs e)
            {
            Point p = Mouse.GetPosition(CanvasBoard);
            double x = p.X;
            double y = p.Y;
            Ellipse piece = CarromPieces.CreatePiece(x, y, Colors.Red, Colors.Black);
            CanvasBoard.Children.Add(piece);
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
            }

        private void GetNextPoint(double x1, double y1, double x2, double y2)
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

        //// New way Working
        //double m = ((lineY1 - lineY2) / (lineX1 - lineX2));
        //double c = (lineY1 - (lineX1 * m));
        //double moveX = lineX1;
        //double moveY = lineY1;

        //if ( lineX2 > moveX )
        //    {
        //    while ( moveX < lineX2 )
        //        {
        //        moveX = moveX + 1;
        //        moveY = ((m * moveX) + c);
        //        //if ( striker1 != null)
        //        //    {
        //        //    System.Threading.Thread.Sleep(50);
        //        //    //CanvasBoard.Children.Remove(striker1);
        //        //    }
        //        striker1 = CreateAnEllipse(moveX, moveY, Colors.Red);
        //        CanvasBoard.Children.Add(striker1);
        //        //System.Threading.Thread.Sleep(50);
        //        //CanvasBoard.Children.Remove(striker1);
        //        if ( moveX > lineX2 )
        //            break;
        //        }
        //    }
        //else if(lineX2 < moveX)
        //    {
        //    while ( lineX2 < moveX )
        //        {
        //        moveX = moveX - 1;
        //        moveY = ((m * moveX) + c);
        //        //if ( striker1 != null)
        //        //    {
        //        //    System.Threading.Thread.Sleep(50);
        //        //    //CanvasBoard.Children.Remove(striker1);
        //        //    }
        //        striker1 = CreateAnEllipse(moveX, moveY, Colors.Red);
        //        CanvasBoard.Children.Add(striker1);
        //        //System.Threading.Thread.Sleep(50);
        //        //CanvasBoard.Children.Remove(striker1);
        //        if ( lineX2 > moveX )
        //            break;
        //        }
        //    }



        // Old code
        //double negDX = (lineX2 - lineX1);
        //double DX = Math.Abs(negDX);
        //double negDY = (lineY2 - lineY1);
        //double DY = Math.Abs(negDY);

        //double speed = Math.Sqrt((Math.Pow(DX, 2)) + (Math.Pow(DY, 2)));

        ////double sinTheta = 0;
        ////double cosTheta = 0;
        ////double dist = (DX % DY);

        ////if ( dist == 0 )
        ////    {
        ////    cosTheta = 1;
        ////    sinTheta = 0;
        ////    }
        ////else
        ////    {
        ////    cosTheta = Math.Abs((DX) / dist);
        ////    sinTheta = Math.Abs((DY) / dist);
        ////    }

        //double theta = Math.Abs(Math.Atan2(DY, DX));
        //double sinTheta = Math.Abs(Math.Sin(theta));
        //double cosTheta = Math.Abs(Math.Cos(theta));

        //double moveX = lineX1;
        //double moveY = lineY1;

        //while ( ((moveX < lineX2) || (moveY != lineY2)))
        //    {
        //    //moveX = (speed * sinTheta);
        //    //moveY = (speed * cosTheta);

        //    //strikerWithDefaultLocation.Margin = new Thickness()
        //    //    {
        //    //    Left = strikerWithDefaultLocation.Margin.Left,
        //    //    Top = strikerWithDefaultLocation.Margin.Top + moveY,
        //    //    Right = strikerWithDefaultLocation.Margin.Right + moveX,
        //    //    Bottom = strikerWithDefaultLocation.Margin.Bottom
        //    //    };
        //    moveX = moveX + (10 * sinTheta);
        //    moveY = moveY + (10 * cosTheta);

        //    CreateAnEllipse(moveX, moveY, Colors.Red);
        //    }

        //double DX1 = (lineX2 - lineX1);
        //double negDY1 = (lineY2 - lineY1);
        //double DY1 = Math.Abs(negDY1);
        }

        public double ConvertDegreesToRadians (double degrees)
            {
            double radians = (Math.PI / 180) * degrees;
            MessageBox.Show(degrees + " " + radians);
            return (radians);
            }

        private void CanvasBoard_OnMouseLeftButtonUp (object sender, MouseButtonEventArgs e)
        {
            GetNextPoint(strikerWithDefaultLocation.Margin.Left,
                 strikerWithDefaultLocation.Margin.Top, lastPoint.X, lastPoint.Y);

}
        }
    }
