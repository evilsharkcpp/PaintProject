using DataStructures.Geometry;

namespace Interfaces
{
    [Flags]
    public enum FigureChangeDirection
    {
        None = 0,
        Top = 1,
        Right = 2,
        Bottom = 4,
        Left = 8,
    }

    public interface IFigureBound
    {
        Point2d Position { get; }
        Vector2d Size { get; }
         public bool IsManySelect { get; }
        double Angle { get; }
        double Padding { get; set; }

        bool OnBound(Point2d point, float eps);
        bool OnBound(Point2d point, float eps, out FigureChangeDirection direction);

        void Translate(Vector2d delta, FigureChangeDirection direction);
        void Resize(Vector2d delta, FigureChangeDirection direction);
        void Rotate(Point2d start, Vector2d delta);
    }
}
