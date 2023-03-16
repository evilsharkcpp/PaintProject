using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DataStructures.Geometry;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace IO.Converted
{
    [DataContract(Name = "Circle")]
    public class ConvertedCircle : ConvertedFigure
    {
        [DataMember(Name = "Center")]
        public Point2d Center;

        [DataMember(Name = "Radius")]
        public double Radius;


        public ConvertedCircle(Point2d center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public SvgCircle toSVG()
        {
            double cx = Center.X;
            double cy = Center.Y; 
            double r = Radius;

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
