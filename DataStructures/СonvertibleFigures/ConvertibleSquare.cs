using DataStructures.Geometry;
using System.Runtime.Serialization;

namespace DataStructures.ConvertibleFigures
{
    [DataContract(Name = "Square")]
    public class ConvertibleSquare : ConvertibleFigure
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


        public ConvertibleSquare(Point2d point1, double width, double height, double angle, Color color)
        {
            this.point1 = point1;
            this.width = width;
            this.height = height;
            this.angle = angle;
            this.color = color;
        }
    }
}
