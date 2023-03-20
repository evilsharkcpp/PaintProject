using DataStructures.Geometry;
using Geometry.Attributes;
using Interfaces;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    [Figure("Triangle")]
    public class Triangle : Figure
    {
        protected static Point2d Point1 = new Point2d(-1, -1);
        protected static Point2d Point2 = new Point2d(1, -1);
        protected static Point2d Point3 = new Point2d(0, 1);

        protected static Vector2d V12;
        protected static Vector2d V23;
        protected static Vector2d V31;

        protected static Vector2d n12;
        protected static Vector2d n23;
        protected static Vector2d n31;

        static Triangle()
        {
            V12 = Point2 - Point1;
            V23 = Point3 - Point2;
            V31 = Point1 - Point3;

            n12.X = V12.Y;
            n12.Y = -V12.X;
            n12.Normilize();

            n23.X = V23.Y;
            n23.Y = -V23.X;
            n23.Normilize();

            n31.X = V31.Y;
            n31.Y = -V31.X;
            n31.Normilize();
        }

        public Triangle() { }

        public Triangle(Triangle triangle) : base(triangle) { }

        protected override void OnDraw(IGraphics graphics)
        {
            graphics.DrawTriangle(Point1, Point2, Point3, false, true);
        }

        protected override bool IsInside(Point2d p, double eps)
        {
            double Q(Point2d a, Point2d b, Point2d p)
            {
                return p.X * (b.Y - a.Y) + p.Y * (a.X - b.X) + a.Y * b.X - a.X * b.Y;
            }

            double q1, q2, q3;
            q1 = Q(Point1, Point2, p);
            q2 = Q(Point2, Point3, p);
            q3 = Q(Point3, Point1, p);
            return (q1 <= eps && q2 <= eps && q3 <= eps);
        }

        protected override bool InArea(Rect rect, double eps)
        {
            throw new NotImplementedException();
        }

        public override IFigure Clone()
        {
            return new Triangle(this);
        }

        protected override Path ToPath()
        {
            throw new NotImplementedException();
        }
    }
}
