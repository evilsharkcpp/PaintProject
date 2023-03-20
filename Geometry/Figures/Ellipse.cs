using DataStructures;
using DataStructures.ConvertibleFigures;
using DataStructures.Geometry;
using Geometry.Attributes;
using Interfaces;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    [Figure("Ellipse")]
    public class Ellipse : Figure
    {
        protected static Point2d Center = new Point2d(0, 0);
        protected static double Radius = 1;

        static Ellipse()
        {

            Center.X = -Radius;
            Center.Y = -Radius;
        }

        public Ellipse() { }

        public Ellipse(Ellipse ellipse) : base(ellipse) { }

        protected override void OnDraw(IGraphics graphics)
        {
            graphics.DrawEllipse(Center, Radius, Radius, false, true);
        }

        protected override bool IsInside(Point2d p, double eps)
        {
            return eps >= 0 && Math.Abs(p.X * p.X + p.Y * p.Y - 1) > eps;
        }

        protected override bool InArea(Rect rect, double eps)
        {
            throw new NotImplementedException();
        }

        public override IFigure Clone()
        {
            return new Ellipse(this);
        }

        protected override Path ToPath()
        {
            throw new NotImplementedException();
        }

        public override ConvertibleFigure ToConvertibleFigure()
        {
            return new ConvertibleEllipse(Center, Radius, Radius, Angle);
        }
    }
}