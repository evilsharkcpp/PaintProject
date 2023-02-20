using Interfaces;
using System.Numerics;

namespace Geometry.Figures
{
    public class Line : IFigure
    {
        public IEnumerable<(string, object)> Parameters => throw new NotImplementedException();

        public IFigure Clone()
        {
            throw new NotImplementedException();
        }

        public void Draw(IGraphics graphics) => graphics.DrawLine();

        public bool HasIntersection(IFigure figure)
        {
            throw new NotImplementedException();
        }

        public IFigure Intersect(IFigure second)
        {
            throw new NotImplementedException();
        }

        public bool IsInside(Vector2 p, double eps)
        {
            throw new NotImplementedException();
        }

        public void Rotate(double angle)
        {
            throw new NotImplementedException();
        }

        public void Scale(double x, double y)
        {
            throw new NotImplementedException();
        }

        public IFigure Subtruct(IFigure second)
        {
            throw new NotImplementedException();
        }

        public void Translate(Vector2 to)
        {
            throw new NotImplementedException();
        }

        public bool TrySetParameter(string name, object value)
        {
            throw new NotImplementedException();
        }

        public IFigure Union(IFigure second)
        {
            throw new NotImplementedException();
        }

        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }
    }
}
