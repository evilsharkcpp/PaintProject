using DataStructures.Geometry;
using System.Runtime.Serialization;

namespace DataStructures.ConvertibleFigures
{
    [DataContract(Name = "FilledCircle")]
    public class ConvertibleFilledCircle : ConvertibleFigure
    {
        // Координаты центра
        [DataMember(Name = "Center")]
        public Point2d center;

        // Длина радиуса
        [DataMember(Name = "Radius")]
        public double radius;

        // Цвет круга
        [DataMember(Name = "FillColor")]
        public Color fill_color;

        public ConvertibleFilledCircle(Point2d center, double radius, double angle, Color color, Color fill_color)
        {
            this.center = center;
            this.radius = radius;
            this.angle = angle;

            this.color = color;
            this.fill_color = fill_color;
        }
    }
}
