using System.Numerics;

namespace Interfaces
{
    public interface IGraphics
    {
        IDrawable Style { get; }
        void DrawLine(Vector2 v1, Vector2 v2, bool isFill, bool isOutLine);
        void DrawRect(Vector2 v1, Vector2 v2, bool isFill, bool isOutLine);
        void DrawTriangle(Vector2 v1, Vector2 v2, Vector2 v3, bool isFill, bool isOutLine);
        void DrawCircle(Vector2 center, double radius, bool isFill, bool isOutLine);
        void DrawEllipse(Vector2 center, double a, double b, bool isFill, bool isOutLine);
        void DrawPolygon(IEnumerable<Vector2> points, bool isFill, bool isOutLine);

    }
}