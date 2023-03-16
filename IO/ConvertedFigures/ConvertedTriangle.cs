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

namespace IO.ConvertedFigures
{
    [DataContract(Name = "Triangle")]
    public class ConvertedTriangle
    {
        [DataMember(Name = "Point1")]
        public Point2d point1;

        [DataMember(Name = "Point2")]
        public Point2d point2;

        [DataMember(Name = "Point3")]
        public Point2d point3;

        public ConvertedTriangle(Point2d p1, Point2d p2, Point2d p3)
        {
            point1 = p1;
            point2 = p2;
            point3 = p3;
        }

        public SvgElement toSVG()
        {
            double x1 = point1.X;
            double y1 = point1.Y;

            double x2 = point2.X;
            double y2 = point2.Y;

            double x3 = point3.X;
            double y3 = point3.Y;


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
