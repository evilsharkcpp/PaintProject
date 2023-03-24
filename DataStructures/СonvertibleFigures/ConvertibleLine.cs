using System.Runtime.Serialization;
using DataStructures.Geometry;

namespace DataStructures.ConvertibleFigures
{
    [DataContract(Name = "Line")]
    public class ConvertibleLine : ConvertibleFigure
    {
        // Cтартовая точка
        [DataMember(Name = "Point1")]
        public Point2d point1;

        // Конечная точка
        [DataMember(Name = "Point2")]
        public Point2d point2;


        public ConvertibleLine(Point2d point1, Point2d point2, double angle)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.angle = angle;

            Height = Math.Abs(point1.Y - point2.Y);
            Width = Math.Abs(point1.X - point2.X);

            position = new Point2d(point1.X, point1.Y - Height);
        }
    }
}
