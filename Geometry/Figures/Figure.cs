using DataStructures.Geometry;
using Geometry.Parameterization;
using Interfaces;
using System.Numerics;

namespace Geometry.Figures
{
    public abstract class Figure : ParameterizedObject, IFigure
    {
        private double _angle;

        public abstract Point2d Center { get; }

        public double Angle
        {
            get => _angle;
            set
            {
                if (_angle != value)
                {
                    _angle = value;

                }
            }
        }

        public abstract void Draw(IGraphics graphics);

        public abstract bool IsInside(Vector2 p, float eps);
        public abstract void Rotate(float angle);
        public abstract void Scale(float x, float y);
        public abstract void Translate(Vector2 to);

        public abstract bool HasIntersection(IFigure figure);
        public abstract IFigure Intersect(IFigure second);
        public abstract IFigure Subtruct(IFigure second);
        public abstract IFigure Union(IFigure second);

        public abstract IFigure Clone();
        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
