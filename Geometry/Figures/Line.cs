using Interfaces;
using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry.Figures
{
    internal class Line : IFigure
    {
        public void Draw(IGraphics graphics) => graphics.DrawLine();

        public bool HasIntersection(IFigure figure)
        {
            throw new NotImplementedException();
        }

        public IFigure Intersect(IFigure second)
        {
            throw new NotImplementedException();
        }

        public bool IsInside(Vector2D p, double eps)
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

        public void Translate(Vector2D to)
        {
            throw new NotImplementedException();
        }

        public IFigure Union(IFigure second)
        {
            throw new NotImplementedException();
        }
    }
}
