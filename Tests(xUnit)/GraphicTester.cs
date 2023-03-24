using DataStructures.Geometry;
using Interfaces;
using Tests_xUnit_.Figures;

namespace Tests_xUnit_
{
    internal class GraphicTester : IGraphics
    {
        public List<object> Figures { get; set; } = new List<object>();

        public IDrawable GraphicStyle
        {
            get;
            set;
        }

        public Matrix3d ModelMatrix
        {
            get;
            set;
        }

        public void DrawEllipse(Point2d start, double a, double b, bool isFill, bool isOutLine)
        {
            Point2d newStart = new Point2d();
            Vector2d v1 = new Vector2d(a, 0),
                     v2 = new Vector2d(0, b),
                     v3 = new Vector2d(1, 0);
            ModelMatrix.Product(start, ref newStart);
            ModelMatrix.Product(v1, ref v1);
            ModelMatrix.Product(v2, ref v2);
            ModelMatrix.Product(v3, ref v3);
            Figures.Add(new Ellipse() { Start = newStart, a = v1.Norm, b = v2.Norm, angle = Math.Asin(new Vector2d(1, 0) ^ v3 / v3.Norm), IsFill = isFill, IsOutline = isOutLine });
        }

        public void DrawLine(Point2d v1, Point2d v2, bool isFill, bool isOutLine)
        {
            Point2d newV1 = new Point2d(), newV2 = new Point2d();
            ModelMatrix.Product(v1, ref newV1);
            ModelMatrix.Product(v2, ref newV2);
            Figures.Add(new Line() { V1 = newV1, V2 = newV2, IsFill = isFill, IsOutline = isOutLine });
        }

        public void DrawPolygon(IEnumerable<Point2d> points, bool isFill, bool isOutLine)
        {
            List<Point2d> newPoints = new List<Point2d>();
            Point2d p = new Point2d();
            foreach (Point2d point in points)
            {
                ModelMatrix.Product(point, ref p);
                newPoints.Add(p);
            }

            Figures.Add(new Polygon() { Points = newPoints, IsFill = isFill, IsOutline = isOutLine });
        }

        public void DrawRectangle(Point2d start, double a, double b, bool isFill, bool isOutLine)
        {
            Point2d newStart = new Point2d();
            Vector2d v1 = new Vector2d(a, 0),
                     v2 = new Vector2d(0, b),
                     v3 = new Vector2d(1, 0);
            ModelMatrix.Product(start, ref newStart);
            ModelMatrix.Product(v1, ref v1);
            ModelMatrix.Product(v2, ref v2);
            ModelMatrix.Product(v3, ref v3);
            Figures.Add(new Rectangle() { Start = newStart, a = v1.Norm, b = v2.Norm, angle = Math.Asin(new Vector2d(1, 0) ^ v3 / v3.Norm), IsFill = isFill, IsOutline = isOutLine });
        }

        public void DrawTriangle(Point2d v1, Point2d v2, Point2d v3, bool isFill, bool isOutLine)
        {
            Point2d newV1 = new Point2d(), newV2 = new Point2d(), newV3 = new Point2d();
            ModelMatrix.Product(v1, ref newV1);
            ModelMatrix.Product(v2, ref newV2);
            ModelMatrix.Product(v3, ref newV3);
            Figures.Add(new Triangle() { V1 = newV1, V2 = newV2, V3 = newV3, IsFill = isFill, IsOutline = isOutLine });
        }
    }
}