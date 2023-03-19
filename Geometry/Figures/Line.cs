using DataStructures.Geometry;
using Geometry.Attributes;
using Interfaces;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    [Figure("Line")]
    public class Line : Figure
    {
        protected static Point2d Point1 = new Point2d(-1, -1);
        protected static Point2d Point2 = new Point2d(1, 1);

        protected static Vector2d V;

        protected static Vector2d n;

        static Line()
        {
            V = Point2 - Point1;

            n.X = V.Y;
            n.Y = -V.X;
            n.Normilize();
        }

        public Line() { }

        public Line(Line line) : base(line) { }

        protected override void OnDraw(IGraphics graphics)
        {
            graphics.DrawLine(Point1, Point2, true, false);
        }

        protected override bool IsInside(Point2d p, double eps)
        {
            Vector2d u = new Vector2d()
            {
                X = 1 - p.X,
                Y = 1 - p.Y
            };
            double l = u * V,
                   h = u * n;
            return Math.Abs(h) <= eps && l / V.Norm >= -eps && l - 1 <= eps;
        }

        protected override bool InArea(Rect rect, double eps)
        {
            throw new NotImplementedException();
        }

        public override IFigure Clone()
        {
            return new Line(this);
        }

        protected override Path ToPath()
        {
            throw new NotImplementedException();
        }
    }
}
