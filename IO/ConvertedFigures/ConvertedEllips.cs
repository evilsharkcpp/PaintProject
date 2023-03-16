using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace IO.ConvertedFigures
{
    [DataContract(Name = "Ellips")]
    public class ConvertedEllips
    {
        public SvgEllipse Ellips(double cx, double cy, double rx, double ry)
        {

            return new SvgEllipse
            {
                CenterX = (SvgUnit)cx,
                CenterY = (SvgUnit)cy,
                RadiusX = (SvgUnit)rx,
                RadiusY = (SvgUnit)ry,

                Stroke = new SvgColourServer(Color.Black)
            };
        }
    }
}
