
using System.Drawing;
using DataStructures.Geometry;
using System.Runtime.Serialization;

namespace DataStructures.СonvertibleFigures
{
    [DataContract(Name = "Circle")]
    public class СonvertibleCircle : ConvertibleFigure
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

        public СonvertibleCircle(Point2d center, double radius, Color color)
        {
            this.center = center;
            this.radius = radius;
            this.color = color;
        }
    }
}
