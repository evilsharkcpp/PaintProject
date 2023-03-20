using Geometry.Attributes;
using Interfaces;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    [Figure("FilledEllipse")]
    internal class FilledEllipse : Ellipse
    {
        public FilledEllipse()
        {
            _bindSize = true;
        }

        public FilledEllipse(FilledEllipse filledEllipse) : base(filledEllipse)
        {
            _bindSize = true;
        }

        public override IFigure Clone()
        {
            return new FilledEllipse(this);
        }

        protected override void OnDraw(IGraphics graphics)
        {
            graphics.DrawEllipse(Center, Radius, Radius, true, true);
        }
    }
}
