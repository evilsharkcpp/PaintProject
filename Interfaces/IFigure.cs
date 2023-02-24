using DataStructures.Geometry;
using System.Numerics;

namespace Interfaces
{
    public interface IFigure : ICloneable
    {
        bool HasIntersection(IFigure figure);
        void Rotate(float angle);
        void Translate(Vector2 to);
        void Scale(float x, float y);
        new IFigure Clone();
        IFigure Intersect(IFigure second);
        IFigure Union(IFigure second);
        IFigure Subtruct(IFigure second);
        void Draw(IGraphics graphics);
        bool IsInside(Vector2 p, float eps);
        IEnumerable<IParameter<double>> DoubleParameters { get; }
        IEnumerable<IParameter<Point2d>> PointParameters { get; }
        IEnumerable<IParameter<Vector2d>> VectorParameters { get; }
        Point2d Center { get; }
    }
}
