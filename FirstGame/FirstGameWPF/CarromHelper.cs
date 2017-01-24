using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
namespace FirstGameWPF
    {
    public class CarromHelper
        {
        public static Duration GetStrikerSpeed (double distance)
            {
            Duration speed;
            
            if (distance > 0 && distance < 100)
                speed = new Duration(TimeSpan.FromMilliseconds(1000));
            else if(distance >= 100 && distance < 200)
                speed = new Duration(TimeSpan.FromMilliseconds(800));
            else if ( distance >= 200 && distance < 300 )
                speed = new Duration(TimeSpan.FromMilliseconds(600));
            else if ( distance >= 300 && distance < 400 )
                speed = new Duration(TimeSpan.FromMilliseconds(400));
            else
                speed = new Duration(TimeSpan.FromMilliseconds(200));

            return speed;
            }

        public static string GetSpeedIndicator (Line redLine)
        {
            string speedText = string.Empty;
            double distance = Math.Sqrt(Math.Pow((redLine.X2 - redLine.X1), 2) + (Math.Pow((redLine.Y2 - redLine.Y1), 2)));
            Duration speed = GetStrikerSpeed(distance);
            if (speed.TimeSpan == TimeSpan.FromMilliseconds(1000))
                speedText = "1x";
            else if ( speed.TimeSpan == TimeSpan.FromMilliseconds(800) )
                speedText = "2x";
            else if ( speed.TimeSpan == TimeSpan.FromMilliseconds(600) )
                speedText = "3x";
            else if ( speed.TimeSpan == TimeSpan.FromMilliseconds(400) )
                speedText = "4x";
            else
                speedText = "5x";

            return speedText;
        }
        }
    }
