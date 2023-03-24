using DataStructures.ConvertibleFigures;
using DataStructures;
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
            Vector2d u = new Vector2d() //вектор из левой верхней точки в определяемую точку
            {
                X = Point2.X - p.X,
                Y = Point2.Y - p.Y
            };

            double s = V.X * u.Y - u.X * V.Y; //удвоенная площадь треугольника, составленного тремя точками, и равна нулю если три точки лежат на одной прямой.
            double norm_line = Math.Sqrt(V.X * V.X + V.Y * V.Y); //длина отрезка
            double h = s / norm_line; //расстояние от точки до  отрезка

            bool on_line = (Math.Abs(h) <= eps); //точка лежит на прямой
            if (on_line)
            {
                if (p.X <= 1 + eps && p.X >= -1 - eps && p.Y <= 1 + eps && p.Y >= -1 - eps) //точка внутри отрезка
                {
                    return true;
                }
                else return false;

            }
            else return false;
        }

        protected override bool OnBound(Point2d p, double eps) => IsInside(p, eps);

        public override IFigure Clone()
        {
            return new Line(this);
        }

        protected override Path ToPath()
        {
            throw new NotImplementedException();
        }

        public override ConvertibleFigure ToConvertibleFigure()
        {
            Point2d p2 = Position + Size;
            return new ConvertibleLine(Position, p2, Angle);
        }
    }
}
