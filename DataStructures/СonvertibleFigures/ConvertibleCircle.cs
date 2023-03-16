
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

        // Длина радиуCа
        [DataMember(Name = "Radius")]
        public double radius;

        // Цвет границы круга
        [DataMember(Name = "Color")]
        public Color color;

        public ConvertibleCircle(Point2d center, double radius, Color color)
        {
            this.center = center;
            this.radius = radius;
            this.color = color;
        }
    }
}
