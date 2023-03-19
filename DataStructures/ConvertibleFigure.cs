using DataStructures.Geometry;
using System.Runtime.Serialization;

namespace DataStructures
{
    [DataContract(Name = "ConvertibleFigure")]
    public abstract class ConvertibleFigure
    {
        // Угол поворота
        [DataMember(Name = "Angle")]
        public double angle;

        // Цвет границы
        [DataMember(Name = "Color")]
        public Color color;
    }
}
