using DataStructures.Geometry;
using System.Runtime.Serialization;

namespace DataStructures.СonvertibleFigures
{
    [DataContract(Name = "FilledCircle")]
    public class СonvertibleFilledCicrle : ConvertibleFigure
    {
        // Координаты центра
        [DataMember(Name = "Center")]
        public Point2d center;

        // Длина радиуса
        [DataMember(Name = "Radius")]
        public double radius;

        // Цвет границы круга
        [DataMember(Name = "Color")]
        public Color color;

        // Цвет круга
        [DataMember(Name = "FillColor")]
        public Color fill_color;

        public СonvertibleFilledCicrle(Point2d center, double radius, Color color, Color fill_color)
        {
            this.center = center;
            this.radius = radius;
            this.color = color;
            this.fill_color = fill_color;
        }
    }
}
