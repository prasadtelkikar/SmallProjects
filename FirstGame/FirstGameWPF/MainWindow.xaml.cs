using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private Line redLine = null;
        private Ellipse strikerWithDefaultLocation = null;
        private Point lastPoint,  startPoint;
        private EllipseGeometry myEllipseGeometry;
        private BackgroundWorker bw;
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
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            }




        private void CanvasBoard_OnMouseLeftButtonDown (object sender, MouseButtonEventArgs e)
            {
            Point p = Mouse.GetPosition(CanvasBoard);
            double x = p.X;
            double y = p.Y;
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
            startPoint = new Point(strikerWithDefaultLocation.Margin.Left,
                 strikerWithDefaultLocation.Margin.Top);
            lastPoint = Mouse.GetPosition(CanvasBoard);

             bw.RunWorkerAsync();

            }
        void bw_RunWorkerCompleted (object sender, RunWorkerCompletedEventArgs e)
            {
            Thread.Sleep(100);
             CanvasBoard.Children.Remove(strikerWithDefaultLocation);
            strikerWithDefaultLocation = CarromPieces.CreateDefaultStriker();
            CanvasBoard.Children.Add(strikerWithDefaultLocation);
            }

        void bw_ProgressChanged (object sender, ProgressChangedEventArgs e)
        {
            string nextXY = e.UserState.ToString();
            double nextX = Convert.ToDouble(nextXY.Split(',')[0]);
            double nextY = Convert.ToDouble(nextXY.Split(',')[1]);
            strikerWithDefaultLocation.Margin = new Thickness()
            {
                Left = nextX,
                Top = nextY,
                Right = strikerWithDefaultLocation.Margin.Right,
                Bottom = strikerWithDefaultLocation.Margin.Bottom
            };
        }

        void bw_DoWork (object sender, DoWorkEventArgs e)
            {

            double m = ((lastPoint.Y - startPoint.Y) / (lastPoint.X - startPoint.X));
            double c = (startPoint.Y - (startPoint.X * m));
            double moveX = startPoint.X;
            double moveY = startPoint.Y;
            if ( lastPoint.X > moveX )
                {
                while ( moveX < lastPoint.X )
                    {
                    moveX = moveX + 1;
                    moveY = ((m * moveX) + c);
                    Thread.Sleep(new TimeSpan(0, 0, 0, 0, 1));
                    bw.ReportProgress(1, moveX + "," + moveY);

                    if ( moveX > lastPoint.X )
                        break;
                    }
                }
            else if ( lastPoint.X < moveX )
                {
                while ( lastPoint.X < moveX )
                    {
                    moveX = moveX - 1;
                    moveY = ((m * moveX) + c);
                    Thread.Sleep(new TimeSpan(0, 0, 0, 0, 1));
                    bw.ReportProgress(1, moveX + "," + moveY);

                    if ( lastPoint.X > moveX )
                        break;
                    }
                }
            }
        }
    }