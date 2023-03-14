using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace IO.SVGFigures
{
    public class SVGCircle
    {
        public SvgCircle Line(double cx, double cy, double r)
        {

            return new SvgCircle
            {
                CenterX = (SvgUnit)cx,
                CenterY = (SvgUnit)cy,
                Radius = (SvgUnit)r,

                Stroke = new SvgColourServer(Color.Black)
            };
        }
    }
}
