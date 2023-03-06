using DataStructures.Geometry;
using System.Numerics;

namespace Interfaces
{
    public interface IFigure : ICloneable
    {
        float Width { get; set; }
        float Height { get; set; }
        float Angle { get; set; }
        IList<Point2d> Points { get; set; }
        bool HasIntersection(IFigure figure);
        new IFigure Clone();
        IFigure Intersect(IFigure second);
        IFigure Union(IFigure second);
        IFigure Subtruct(IFigure second);
        void Draw(IGraphics graphics);
        bool IsInside(Vector2 p, float eps);
        Point2d Center { get; }
    }
}
