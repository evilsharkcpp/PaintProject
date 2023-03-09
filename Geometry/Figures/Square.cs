using Geometry.Attributes;
using Interfaces;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    [Figure("Square")]
    public class Square : Rectangle
    {
        public Square()
        {
            _bindSize = true;
        }

        public Square(Square square) : base(square)
        {
            _bindSize = true;
        }

        public override IFigure Clone()
        {
            return new Square(this);
        }
    }
}
