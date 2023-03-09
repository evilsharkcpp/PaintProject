using Geometry.Attributes;
using Interfaces;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    [Figure("FilledCircle")]
    internal class FilledCircle : Circle
    {
        public FilledCircle()
        {
            _bindSize = true;
        }

        public FilledCircle(FilledCircle filledCircle) : base(filledCircle)
        {
            _bindSize = true;
        }

        public override IFigure Clone()
        {
            return new FilledCircle(this);
        }

        protected override void OnDraw(IGraphics graphics)
        {
            graphics.DrawEllipse(Center, Radius, Radius, true, true);
        }
    }
}
