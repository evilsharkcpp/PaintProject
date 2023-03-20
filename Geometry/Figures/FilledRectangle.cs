using Geometry.Attributes;
using Interfaces;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    [Figure("FilledRectangle")]
    internal class FilledRectangle: Rectangle
    {
        public FilledRectangle()
        {
            _bindSize = true;
        }

        public FilledRectangle(FilledRectangle filledRectangle) : base(filledRectangle)
        {
            _bindSize = true;
        }

        public override IFigure Clone()
        {
            return new FilledRectangle(this);
        }

        protected override void OnDraw(IGraphics graphics)
        {
            graphics.DrawRectangle(Position, Width, Height, true, true);
        }
    }
}
