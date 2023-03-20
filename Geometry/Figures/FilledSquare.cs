using Geometry.Attributes;
using Interfaces;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    [Figure("FilledSquare")]
    internal class FilledSquare : Square
    {
        public FilledSquare()
        {
            _bindSize = true;
        }

        public FilledSquare(FilledSquare filledSquare) : base(filledSquare)
        {
            _bindSize = true;
        }

        public override IFigure Clone()
        {
            return new FilledSquare(this);
        }

        protected override void OnDraw(IGraphics graphics)
        {
            graphics.DrawRectangle(Position, Width, Height, true, true);
        }
    }
}
