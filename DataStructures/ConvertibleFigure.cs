using DataStructures.Geometry;
using System.Runtime.Serialization;

namespace DataStructures
{
    [DataContract(Name = "ConvertibleFigure")]
    public abstract class ConvertibleFigure
    {
        // Позиция фигуры
        [DataMember(Name = "Position")]
        public Point2d position;

        // Угол поворота
        [DataMember(Name = "Angle")]
        public double angle;

        // Высота
        [DataMember(Name = "Height")]
        public double Height;

        // Ширина
        [DataMember(Name = "Width")]
        public double Width;

        // Заполненность
        [DataMember(Name = "IsFilled")]
        public bool IsFilled = false;
    }
}
