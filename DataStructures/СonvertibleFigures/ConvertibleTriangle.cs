using DataStructures.Geometry;
using System.Runtime.Serialization;

namespace DataStructures.ConvertibleFigures
{
    [DataContract(Name = "Triangle")]
    public class ConvertibleTriangle : ConvertibleFigure
    {
        // Первая точка
        [DataMember(Name = "Point1")]
        public Point2d point1;

        // Вторая точка (Вершина)
        [DataMember(Name = "Point2")]
        public Point2d point2;

        // Третья точка
        [DataMember(Name = "Point3")]
        public Point2d point3;

        public ConvertibleTriangle(Point2d point1, Point2d point2, Point2d point3, double angle)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
            this.angle = angle;

            Width = Math.Abs(point1.X - point3.X);
            Height = Math.Abs(point2.Y - point1.Y);

            position = new Point2d(point1.X, point1.Y);
        }

    }
}
