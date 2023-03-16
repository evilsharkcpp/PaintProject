using System.Runtime.Serialization;
using DataStructures.Geometry;

namespace DataStructures.СonvertibleFigures
{
    [DataContract(Name = "Line")]
    public class ConvertibleLine : ConvertibleFigure
    {
        // Стартовая точка
        [DataMember(Name = "Point1")]
        public Point2d point1;

        // Конечная точка
        [DataMember(Name = "Point2")]
        public Point2d point2;

        // Цвет линии
        [DataMember(Name = "Color")]
        public Color color;

        public ConvertibleLine(Point2d point1, Point2d point2, Color color )
        {
            this.point1 = point1;
            this.point2 = point2;
            this.color = color;
        }
    }
}
