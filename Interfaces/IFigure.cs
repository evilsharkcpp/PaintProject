using DataStructures.Geometry;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Numerics;

namespace Interfaces
{
    public interface IFigure : ICloneable
    {
        Vector2d Size { get; set; }
        double Angle { get; set; }
        [Reactive] Point2d Position { get; set; }

        IReadOnlyList<INode> Nodes { get; }
        IReadOnlyList<IParameter<object>> ExtraProperties { get; }
        IReadOnlyList<(string, ReactiveCommand<Point2d, bool>)> Commands { get; }

        void Draw(IGraphics graphics);

        bool IsInside(Vector2 p, double eps);

        bool HasIntersection(IFigure figure);
        IFigure Intersect(IFigure second);
        IFigure Union(IFigure second);
        IFigure Subtruct(IFigure second);

        new IFigure Clone();
    }
}
