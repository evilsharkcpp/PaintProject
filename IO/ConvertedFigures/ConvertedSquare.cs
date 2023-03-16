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
    [DataContract(Name = "Square")]
    public class ConvertedSquare : ConvertedRectangle
    {
        public ConvertedSquare(Point2d p1, Point2d p2, Point2d p3, Point2d p4) : base(p1, p2, p3, p4)
        {
        }
    }
}
