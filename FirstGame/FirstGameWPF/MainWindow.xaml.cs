using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace FirstGameWPF
    {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
        {

        static double  striker_x = 0;
        static double striker_y = 0;
        private Ellipse strikerWithDefaultLocation = null;

        public MainWindow ()
            {
            InitializeComponent();
            strikerWithDefaultLocation = new Ellipse();
            strikerWithDefaultLocation.Height = 30;
            strikerWithDefaultLocation.Width = 30;
            strikerWithDefaultLocation.Margin = new Thickness(257, 487, 0, 0);
            SolidColorBrush blueBrush = new SolidColorBrush();
            blueBrush.Color = Colors.CadetBlue;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;

            strikerWithDefaultLocation.StrokeThickness = 1;
            strikerWithDefaultLocation.Stroke = blackBrush;
            strikerWithDefaultLocation.Fill = blueBrush;
            CanvasBoard.Children.Add(strikerWithDefaultLocation);
            //Striker --> implement it to move striker

            }
        /// <summary>
        /// Creates a blue ellipse with black border
        /// </summary>
        public void CreateAnEllipse (double height, double width, Color color)
            {


            Ellipse striker = new Ellipse
            {
                Width = 30, Height = 30
            };
            striker_y = height - 15;
            striker_x = width - 15;
            striker.Margin = new Thickness(striker_y, striker_x, 0, 0);
            SolidColorBrush blueBrush = new SolidColorBrush();
            blueBrush.Color = Colors.CadetBlue;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;

            striker.StrokeThickness = 1;
            striker.Stroke = blackBrush;
            striker.Fill = blueBrush;
            CanvasBoard.Children.Add(striker);
            }

        private void CanvasBoard_OnMouseLeftButtonDown (object sender, MouseButtonEventArgs e)
            {
            Point p = e.GetPosition(this);
            double x = p.X;
            double y = p.Y;
            CreateAnEllipse(x, y, Colors.Black);
            MessageBox.Show("X1:" + striker_x + "," + "Y1:" + striker_y);
            MessageBox.Show("X2:" + x + "," + "Y2:" + y);
            Line redLine = new Line();
            redLine.X1 = strikerWithDefaultLocation.Margin.Left + 15;
            redLine.Y1 = strikerWithDefaultLocation.Margin.Top + 15;
            redLine.X2 = p.X;
            redLine.Y2 = p.Y;
            // Create a red Brush
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;

            // Set Line's width and color
            redLine.StrokeThickness = 4;
            redLine.Stroke = redBrush;

            // Add line to the Grid.
            CanvasBoard.Children.Add(redLine);
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
        }
    }
