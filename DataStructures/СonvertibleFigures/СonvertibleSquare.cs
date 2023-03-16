using DataStructures.Geometry;
using System.Runtime.Serialization;

namespace DataStructures.СonvertibleFigures
{
    [DataContract(Name = "Square")]
    public class СonvertibleSquare : ConvertibleFigure
    {
        // Левая верхняя точка
        [DataMember(Name = "Position")]
        public Point2d point1;

        // Ширина квадрата
        [DataMember(Name = "Width")]
        public double width;

        // Ширина квадрата
        [DataMember(Name = "Height")]
        public double height;

        // Цвет границы квадрата
        [DataMember(Name = "Color")]
        public Color color;

        public СonvertibleSquare(Point2d point1, double width, double height, Color color)
        {
            this.point1 = point1;
            this.width = width;
            this.height = height;
            this.color = color;
        }
    }
}
