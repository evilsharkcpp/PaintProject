using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IFigure
    {
        bool HasIntersection(IFigure figure);
        void Rotate(double angle);
        void Translate(Vector2D to);
        void Scale(double x, double y);
        IFigure Intersect(IFigure second);
        IFigure Union(IFigure second);
        IFigure Subtruct(IFigure second);
        void Draw(IGraphics graphics);
        bool IsInside(Vector2D p, double eps);
    }
}
