using DataStructures;
using DataStructures.Geometry;
using Geometry.Attributes;
using Interfaces;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    [Figure("Rectangle")]
    public class Rectangle : Figure
    {
        protected static Point2d Start;
        protected static double Width = 2;
        protected static double Height = 2;

        static Rectangle()
        {
            Start.X = -Width / 2.0;
            Start.Y = -Height / 2.0;
        }

        public Rectangle() { }

        public Rectangle(Rectangle rectangle) : base(rectangle) { }

        protected override void OnDraw(IGraphics graphics)
        {
            graphics.DrawRectangle(Start, Width, Height, true, false);
        }

        protected override bool IsInside(Point2d p, double eps)
        {
            return p.X - Start.X >= -eps &&
                   Start.X + Width - p.X >= -eps &&
                   p.Y - Start.Y >= -eps &&
                   Start.Y + Height - p.Y >= -eps;
        }

        protected override bool InArea(Rect rect, double eps)
        {
            throw new NotImplementedException();
        }

        public override IFigure Clone()
        {
            return new Rectangle(this);
        }

        protected override Path ToPath()
        {
            throw new NotImplementedException();
        }

        public override ConvertibleFigure ToConvertibleFigure()
        {
            throw new NotImplementedException();
        }
    }
}
