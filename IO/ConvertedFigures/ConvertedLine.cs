using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DataStructures.Geometry;
using Svg;
using Svg.Transforms;

namespace IO.ConvertedFigures
{
    [DataContract(Name = "Line")]
    public class ConvertedLine
    {
        [DataMember(Name = "Point1")]
        public Point2d point1;

        [DataMember(Name = "Point2")]
        public Point2d point2;

        public ConvertedLine(Point2d p1, Point2d p2)
        {
            point1 = p1;
            point2 = p2;
        }

        public SvgElement toSVG()
        {
            double x1 = point1.X;
            double y1 = point1.Y;
            double x2 = point2.X;                
            double y2 = point2.Y;


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
