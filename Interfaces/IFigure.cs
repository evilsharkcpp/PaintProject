using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IFigure:ICloneable
    {
        bool HasIntersection(IFigure figure);
        void Rotate(double angle);
        void Translate(Vector2 to);
        void Scale(double x, double y);
        new IFigure Clone();
        IFigure Intersect(IFigure second);
        IFigure Union(IFigure second);
        IFigure Subtruct(IFigure second);
        void Draw(IGraphics graphics);
        bool IsInside(Vector2 p, double eps);
        IEnumerable<(string, object)> Parameters { get; }
        bool TrySetParameter(string name, object value);
    }
}
