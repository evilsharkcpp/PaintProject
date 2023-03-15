using Geometry.Attributes;
using Interfaces;
using ReactiveUI;
using System.Numerics;
using System.Reactive;
using System.Reactive.Linq;
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
    }    
}
