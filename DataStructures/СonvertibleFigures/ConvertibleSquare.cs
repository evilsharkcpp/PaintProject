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

        public ConvertibleSquare(Point2d point1, double width, double height, double angle)
        {
            this.point1 = point1;
            this.angle = angle;

            Width = width;
            Height = height;

            position = point1; 
        }
    }
}
