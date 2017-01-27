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
        private Point lastPoint, startPoint;
        private EllipseGeometry myEllipseGeometry;
        private BackgroundWorker bw;
        private List<string> pocketLocation;

        public MainWindow ()
            {
            InitializeComponent();
            strikerWithDefaultLocation = CarromPieces.CreateDefaultStriker();
            CanvasBoard.Children.Add(strikerWithDefaultLocation);
            //Striker --> implement it to move striker


            myEllipseGeometry = new EllipseGeometry();
            myEllipseGeometry.Center = new Point(strikerWithDefaultLocation.Margin.Left,
                strikerWithDefaultLocation.Margin.Top);
            myEllipseGeometry.RadiusX = 14;
            myEllipseGeometry.RadiusY = 14;
            pocketLocation = GetPocketCoOrdinates();

            //throw new NotImplementedException();
            this.RegisterName("MyEllipseGeometry", myEllipseGeometry);
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            }

        private List<string> GetPocketCoOrdinates ()
            {
            List<string> pocketLocation = new List<string>();
            pocketLocation.Add(Canvas.GetLeft(LeftTopPocket) + "," + Canvas.GetTop(LeftTopPocket));
            pocketLocation.Add(Canvas.GetLeft(LeftBottomPocket) + "," + Canvas.GetTop(LeftBottomPocket));
            pocketLocation.Add(Canvas.GetLeft(RightTopPocket) + "," + Canvas.GetTop(RightTopPocket));
            pocketLocation.Add(Canvas.GetLeft(RightBottomPocket) + "," + Canvas.GetTop(RightBottomPocket));
            return pocketLocation;
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
            if ( strikerWithDefaultLocation.IsVisible )
                {
                CanvasBoard.Children.Remove(strikerWithDefaultLocation);
                strikerWithDefaultLocation = CarromPieces.CreateDefaultStriker();
                CanvasBoard.Children.Add(strikerWithDefaultLocation);
                }
            else
                {
                if ( MessageBox.Show("Dues..!", "Action after dues", MessageBoxButton.OKCancel) == MessageBoxResult.OK )
                    {
                    CanvasBoard.Children.Remove(strikerWithDefaultLocation);
                    strikerWithDefaultLocation = CarromPieces.CreateDefaultStriker();
                    CanvasBoard.Children.Add(strikerWithDefaultLocation);
                    }
                }
            }

        void bw_ProgressChanged (object sender, ProgressChangedEventArgs e)
            {

            string nextXY = e.UserState.ToString();
            double nextX = Convert.ToDouble(nextXY.Split(',')[0]);
            double nextY = Convert.ToDouble(nextXY.Split(',')[1]);
            if ( CheckLocationOfPocket(nextX, nextY) )
                {
                strikerWithDefaultLocation.Visibility = Visibility.Hidden;
                //MessageBox.Show("Disappear");
                }


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
            // Trying with angle
            double negDX = (lastPoint.X - startPoint.X);
            double DX = Math.Abs(negDX);
            double negDY = (lastPoint.Y - startPoint.Y);
            double DY = Math.Abs(negDY);

            double speed = Math.Sqrt((Math.Pow(DX, 2)) + (Math.Pow(DY, 2)));

            double theta = Math.Abs(Math.Atan2(DY, DX));
            double sinTheta = Math.Abs(Math.Sin(theta));
            double cosTheta = Math.Abs(Math.Cos(theta));

            double moveX = startPoint.X;
            double moveY = startPoint.Y;
            double mov = 0.2;


            if ( lastPoint.X > moveX )
                {
                while ( moveX <= lastPoint.X )
                    {
                    moveX = moveX + (mov * cosTheta);
                    moveY = moveY - (mov * sinTheta);

                    Thread.Sleep(20);
                    bw.ReportProgress(1, moveX + "," + moveY);
                    mov += 0.2;
                    }
                }
            else if ( lastPoint.X < moveX )
                {
                while ( moveX >= lastPoint.X )
                    {
                    moveX = moveX - (mov * cosTheta);
                    moveY = moveY - (mov * sinTheta);
                    CheckLocationOfPocket(moveX, moveY);
                    Thread.Sleep(20);
                    bw.ReportProgress(1, moveX + "," + moveY);
                    mov += 1;
                    }
                }
            // Using Slope
            double m = ((lastPoint.Y - startPoint.Y) / (lastPoint.X - startPoint.X));
            double c = (startPoint.Y - (startPoint.X * m));
            moveX = startPoint.X;
            moveY = startPoint.Y;

            if ( lastPoint.X > moveX )
                {
                double oldMoveY = 0;
                double oldMoveX = 0;
                while ( moveX < lastPoint.X )
                    {
                    moveX = moveX + 1;
                    moveY = ((m * moveX) + c);
                    CheckLocationOfPocket(moveX, moveY);
                    Thread.Sleep(20);
                    bw.ReportProgress(1, moveX + "," + moveY);
                    //Ellipse nextMove = CarromPieces.CreatePiece(moveX, moveY, Colors.CadetBlue, Colors.Black);

                    //CanvasBoard.Children.Add(nextMove);

                    //if ( moveX > x2 )
                    //    break;

                    if ( moveX > 514 )
                        {
                        oldMoveY = moveY;
                        oldMoveX = moveX;

                        while ( moveX > 35 && moveY > 39 )
                            {
                            double dist = oldMoveY - startPoint.Y;
                            double totalDist = Math.Abs(dist) + Math.Abs(dist);
                            double latestY = startPoint.Y - totalDist;
                            double latestX = startPoint.X;

                            double latestM = ((latestY - oldMoveY) / (latestX - oldMoveX));
                            double latestC = (oldMoveY - (oldMoveX * latestM));

                            moveX = moveX - 1;
                            moveY = ((latestM * moveX) + latestC);
                            CheckLocationOfPocket(moveX, moveY);
                            Thread.Sleep(20);
                            bw.ReportProgress(1, moveX + "," + moveY);
                            //Ellipse nextMove1 = CarromPieces.CreatePiece(moveX, moveY, Colors.CadetBlue, Colors.Black);
                            //CanvasBoard.Children.Add(nextMove1);
                            }
                        if ( moveX <= 35 )
                            {
                            oldMoveY = moveY;
                            oldMoveX = moveX;

                            while ( moveX < 514 && moveY > 39 )
                                {
                                double dist = oldMoveY - startPoint.Y;
                                double totalDist = Math.Abs(dist) + Math.Abs(dist);
                                double latestY = startPoint.Y - totalDist;
                                double latestX = startPoint.X;

                                double latestM = ((latestY - oldMoveY) / (latestX - oldMoveX));
                                double latestC = (oldMoveY - (oldMoveX * latestM));

                                moveX = moveX + 1;
                                moveY = ((latestM * moveX) + latestC);
                                CheckLocationOfPocket(moveX, moveY);
                                Thread.Sleep(20);
                                bw.ReportProgress(1, moveX + "," + moveY);
                                //Ellipse nextMove1 = CarromPieces.CreatePiece(moveX, moveY, Colors.CadetBlue, Colors.Black);
                                //CanvasBoard.Children.Add(nextMove1);
                                }

                            }
                        break;
                        }
                    }


                }
            else if ( lastPoint.Y < moveX )
                {
                double oldMoveY = 0;
                double oldMoveX = 0;
                while ( lastPoint.X < moveX )
                    {
                    oldMoveY = moveY;
                    oldMoveX = moveX;

                    moveX = moveX - 1;
                    moveY = ((m * moveX) + c);

                    //Ellipse nextMove = CarromPieces.CreatePiece(moveX, moveY, Colors.CadetBlue, Colors.Black);
                    //CanvasBoard.Children.Add(nextMove);

                    Thread.Sleep(20);
                    bw.ReportProgress(1, moveX + "," + moveY);
                    if ( moveX < 35 )
                        {

                        while ( moveX < 514 && moveY > 39 )
                            {
                            double dist = oldMoveY - startPoint.Y;
                            double totalDist = Math.Abs(dist) + Math.Abs(dist);
                            double latestY = startPoint.Y - totalDist;
                            double latestX = startPoint.X;

                            double latestM = ((latestY - oldMoveY) / (latestX - oldMoveX));
                            double latestC = (oldMoveY - (oldMoveX * latestM));

                            moveX = moveX + 1;
                            moveY = ((latestM * moveX) + latestC);

                            //Ellipse nextMove1 = CarromPieces.CreatePiece(moveX, moveY, Colors.CadetBlue, Colors.Black);
                            //CanvasBoard.Children.Add(nextMove1);
                            CheckLocationOfPocket(moveX, moveY);
                            Thread.Sleep(20);
                            bw.ReportProgress(1, moveX + "," + moveY);
                            }

                        if ( moveX >= 514 )
                            {
                            oldMoveY = moveY;
                            oldMoveX = moveX;

                            while ( moveX > 35 && moveY > 39 )
                                {
                                double dist = oldMoveY - startPoint.Y;
                                double totalDist = Math.Abs(dist) + Math.Abs(dist);
                                double latestY = startPoint.Y - totalDist;
                                double latestX = startPoint.X;

                                double latestM = ((latestY - oldMoveY) / (latestX - oldMoveX));
                                double latestC = (oldMoveY - (oldMoveX * latestM));

                                moveX = moveX - 1;
                                moveY = ((latestM * moveX) + latestC);
                                CheckLocationOfPocket(moveX, moveY);
                                //Ellipse nextMove1 = CarromPieces.CreatePiece(moveX, moveY, Colors.CadetBlue, Colors.Black);
                                //CanvasBoard.Children.Add(nextMove1);

                                Thread.Sleep(20);
                                bw.ReportProgress(1, moveX + "," + moveY);
                                }
                            }
                        break;
                        }

                    //if ( x2 > moveX )
                    //    break;
                    }
                }
            }

        private bool CheckLocationOfPocket (double moveX, double moveY)
            {
            foreach ( string location in pocketLocation )
                {
                double x = Convert.ToDouble(location.Split(',')[0]);
                double y = Convert.ToDouble(location.Split(',')[1]);

                if ( x == moveX && y == moveY )
                    return true;

                }
            return false;
            }

        }
    }