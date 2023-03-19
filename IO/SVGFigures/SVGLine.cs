using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Svg;
using Svg.Transforms;

namespace IO.SVGFigures
{
    public class SVGLine
    {
        public SvgLine Line(double x1, double y1, double x2, double y2)
        {

            return new SvgLine
            {
                StartX = (SvgUnit)x1,
                StartY = (SvgUnit)y1,
                EndX = (SvgUnit)x2,
                EndY = (SvgUnit)y2,

                Stroke = new SvgColourServer(Color.Black)
            };
        }
        
    }
}
