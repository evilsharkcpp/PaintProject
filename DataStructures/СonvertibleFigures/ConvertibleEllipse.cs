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

        // Длина радиуса по оси X
        [DataMember(Name = "RadiusX")]
        public double radiusX;

        // Длина радиуса по оси Y
        [DataMember(Name = "RadiusY")]
        public double radiusY;

        public ConvertibleEllipse(Point2d center, double radiusX, double radiusY, double angle, Color color)
        {
            this.center = center;
            this.radiusX = radiusX;
            this.radiusY = radiusY;
            this.angle = angle;
            this.color = color;
        }
    }
}
