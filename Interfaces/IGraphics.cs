using System.Numerics;

namespace Interfaces
{
    public interface IGraphics
    {
        IDrawable Style { get; }
        void DrawLine(Vector2 v1, Vector2 v2);
        void DrawRect(Vector2 v1, Vector2 v2);
        void DrawTriangle(Vector2 v1, Vector2 v2, Vector2 v3);
        void DrawCircle(Vector2 center, double radius, bool isFill);

    }
}