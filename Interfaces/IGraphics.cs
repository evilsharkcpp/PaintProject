using DataStructures.Geometry;

namespace Interfaces
{
    public interface IGraphics
    {
        IDrawable GraphicStyle { get; set; }

        Matrix3d ModelMatrix { get; set; }

        void DrawLine(Point2d v1, Point2d v2, bool isFill, bool isOutLine);
        void DrawTriangle(Point2d v1, Point2d v2, Point2d v3, bool isFill, bool isOutLine);
        void DrawEllipse(Point2d start, double a, double b, bool isFill, bool isOutLine);
        void DrawRectangle(Point2d start, double a, double b, bool isFill, bool isOutLine);
        void DrawPolygon(IEnumerable<Point2d> points, bool isFill, bool isOutLine);
    }
}