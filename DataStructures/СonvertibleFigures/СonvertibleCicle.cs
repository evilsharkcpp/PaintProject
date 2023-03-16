
using System.Drawing;
using DataStructures.Geometry;
using System.Runtime.Serialization;

namespace DataStructures.СonvertibleFigures
{
    [DataContract(Name = "Circle")]
    public struct СonvertibleCircle
    {
        // Координаты центра
        [DataMember(Name = "Center")]
        public Point2d Center;

        // Длина радиуса
        [DataMember(Name = "Radius")]
        public double Radius;

        // Цвет границы круга
        [DataMember(Name = "Color")]
        public Color color;
    }
}
