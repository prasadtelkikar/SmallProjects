using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
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
        }
    }
