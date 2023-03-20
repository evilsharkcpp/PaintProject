using Geometry.Attributes;
using Interfaces;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    [Figure("FilledTriangle")]
    internal class FilledTriangle: Triangle
    {
        public FilledTriangle()
        {
            _bindSize = true;
        }

        public FilledTriangle(FilledTriangle filledTriangle) : base(filledTriangle)
        {
            _bindSize = true;
        }

        public override IFigure Clone()
        {
            return new FilledTriangle(this);
        }

        protected override void OnDraw(IGraphics graphics)
        {
            graphics.DrawTriangle(Point1, Point2, Point3, true, true);
        }
    }
}
