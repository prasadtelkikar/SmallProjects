using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FirstGameWPF
{

    class CarromPieces
    {
        public static Ellipse striker = null;
        List<Ellipse> whiteCoins = null;
        List<Ellipse> blackCoins = null;

        /// <summary>
        /// Generate striker at Default location
        /// </summary>
        /// <returns></returns>
        public static Ellipse CreateDefaultStriker()
        {
            Ellipse striker = new Ellipse();
            striker.Height = 30;
            striker.Width = 30;

            striker.Margin = new Thickness(200, 490, 0, 0);

            SolidColorBrush blueBrush = new SolidColorBrush();
            blueBrush.Color = Colors.CadetBlue;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;

            striker.StrokeThickness = 1;
            striker.Stroke = blackBrush;
            striker.Fill = blueBrush;

            return striker;
        }

        /// <summary>
        /// Draw pieces
        /// </summary>
        public static Ellipse CreatePiece (double height, double width, Color colorFill, Color boarderColor)
        {
            Ellipse piece = new Ellipse
            {
                Width = 20,
                Height = 20
            };
            piece.Margin = new Thickness(height, width, 0, 0);
            SolidColorBrush blueBrush = new SolidColorBrush();
            blueBrush.Color = colorFill;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = boarderColor;

            piece.StrokeThickness = 1;
            piece.Stroke = blackBrush;
            piece.Fill = blueBrush;

            return piece; 
        }
    }

    public class Velocity
    {
        public static float degree = 0;
        private static int speed = 0;

        /// <summary>
        /// Get degree
        /// </summary>
        public static float Degree
        {
            get
            {
                if (degree != 0)
                    return degree;
                else
                    return 0;
            }
        }
        /// <summary>
        /// Get Speed
        /// </summary>
        public static float Speed
            {
            get
                {
                if (speed != 0 )
                    return speed;
                else
                    return 0;
                }
            }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="degree">Degree for direction</param>
        /// <param name="speed">Speed</param>
        public Velocity(float degree, int speed)
        {
            Velocity.degree = degree;
            Velocity.speed = speed;
        }
    }
}