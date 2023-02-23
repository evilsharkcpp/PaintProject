using Interfaces;
using System.Numerics;

namespace Geometry.Figures
{
    public class Line : IFigure
    {
        public IEnumerable<IParameter<float>> FloatParameters => throw new NotImplementedException();

        public IEnumerable<IParameter<Vector2>> Vector2Parameters => throw new NotImplementedException();

        public IFigure Clone()
        {
            throw new NotImplementedException();
        }

        public void Draw(IGraphics graphics)
        {
            throw new NotImplementedException();
        }

        public bool HasIntersection(IFigure figure)
        {
            throw new NotImplementedException();
        }

        public IFigure Intersect(IFigure second)
        {
            throw new NotImplementedException();
        }

        public bool IsInside(Vector2 p, float eps)
        {
            throw new NotImplementedException();
        }

        public void Rotate(float angle)
        {
            throw new NotImplementedException();
        }

        public void Scale(float x, float y)
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
