using DataStructures;
using DataStructures.ConvertibleFigures;
using DataStructures.Geometry;
using Geometry.Attributes;
using Interfaces;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    [Figure("Circle")]
    public class Circle : Ellipse
    {
        public Circle()
        {
            _bindSize = true;
        }

        public Circle(Circle circle) : base(circle)
        {
            _bindSize = true;
        }

        public override IFigure Clone()
        {
            return new Circle(this);
        }

        public override ConvertibleFigure ToConvertibleFigure()
        {
            Point2d center = new Point2d(Position.X + Size.X / 2, Position.Y + Size.Y / 2);

            return new ConvertibleCircle(center, Size.X / 2, Angle);
        }
    }    
}
