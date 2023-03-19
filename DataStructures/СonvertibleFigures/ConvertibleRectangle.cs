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
        [DataMember(Name = "Position")]
        public Point2d point1;

        // Ширина прямоугольника
        [DataMember(Name = "Width")]
        public double width;

        // Ширина прямоугольника
        [DataMember(Name = "Height")]
        public double height;

        public ConvertibleRectangle(Point2d point1, double width, double height, double angle, Color color)
        {
            this.point1 = point1;
            this.width = width;
            this.height = height;
            this.angle = angle;
            this.color = color;
        }
    }
}
