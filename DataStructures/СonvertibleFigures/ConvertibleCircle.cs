
using System.Drawing;
using DataStructures.Geometry;
using System.Runtime.Serialization;

namespace DataStructures.ConvertibleFigures
{
    [DataContract(Name = "Circle")]
    public class ConvertibleCircle : ConvertibleFigure
    {
        // Координаты центра
        [DataMember(Name = "Center")]
        public Point2d center;

        // Длина радиуса
        [DataMember(Name = "Radius")]
        public double radius;


        public ConvertibleCircle(Point2d center, double radius, double angle)
        {
            this.center = center;
            this.radius = radius;
            this.angle = angle;

            Height = radius * 2;
            Width = radius * 2;

            double px = center.X - radius;
            double py = center.Y - radius;

            position = new Point2d(px, py);
        }
    }
}
