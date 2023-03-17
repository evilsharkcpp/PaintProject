using System.Runtime.Serialization;
using DataStructures.Geometry;

namespace DataStructures.ConvertibleFigures
{
    [DataContract(Name = "Ellips")]
    public class ConvertibleEllipse : ConvertibleFigure
    {
        // Координаты центра
        [DataMember(Name = "Center")]
        public Point2d center;

        // Длина радиуCа по оCи X
        [DataMember(Name = "RadiusX")]
        public double radiusX;

        // Длина радиуCа по оCи Y
        [DataMember(Name = "RadiusY")]
        public double radiusY;

        // Цвет границы эллипCа
        [DataMember(Name = "Color")]
        public Color color;

        public ConvertibleEllipse(Point2d center, double radiusX, double radiusY, Color color)
        {
            this.center = center;
            this.radiusX = radiusX;
            this.radiusY = radiusY;
            this.color = color;
        }
    }
}
