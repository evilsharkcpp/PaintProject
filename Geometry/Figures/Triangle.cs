using DataStructures.ConvertibleFigures;
using DataStructures;
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
            double h1 = (p - Point1) ^ V12 / V12.Norm,
                   h2 = (p - Point2) ^ V23 / V23.Norm,
                   h3 = (p - Point3) ^ V31 / V31.Norm;

            return h1 <= eps && h2 <= eps && h3 <= eps;
        }

        public override IFigure Clone()
        {
            return new Triangle(this);
        }

        protected override Path ToPath()
        {
            throw new NotImplementedException();
        }

        public override ConvertibleFigure ToConvertibleFigure()
        {

            return new ConvertibleTriangle(Point1, Point3, Point2, Angle);
        }
    }
}
