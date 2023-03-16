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

namespace DataStructures.СonvertibleFigures
{
    [DataContract(Name = "Rectangle")]
    public class СonvertibleRectangle : ConvertibleFigure
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

        // Цвет границы прямоугольника
        [DataMember(Name = "Color")]
        public Color color;

        public СonvertibleRectangle(Point2d point1, double width, double height, Color color)
        {
            this.point1 = point1;
            this.width = width;
            this.height = height;
            this.color = color;
        }
    }
}
