using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IO.SVGFigures
{
    public class SVGTriangle
    {
        public SvgPolygon Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {


            return new SvgPolygon
            {
                Points = new SvgPointCollection
                {
                    new SvgUnit((float)x1), new SvgUnit((float)y1),
                    new SvgUnit((float)x2), new SvgUnit((float)y2),
                    new SvgUnit((float)x3), new SvgUnit((float)y3)
                },


                Stroke = new SvgColourServer(Color.Black)
            };
        }
    }
}
