using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DataStructures.Geometry;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace DataStructures.ConvertibleFigures
{
    [DataContract(Name = "Rectangle")]
    public class ConvertibleRectangle : ConvertibleFigure
    {
        // Левая верхняя прямоугольника
        [DataMember(Name = "Point1")]
        public Point2d point1;

        public ConvertibleRectangle(Point2d point1, double width, double height, double angle)
        {
            this.point1 = point1;
            this.angle = angle;

            Width = width;
            Height = height;

            position = new Point2d(point1.X, point1.Y - Height);
        }
    }
}
