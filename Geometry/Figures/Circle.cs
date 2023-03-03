using DataStructures.Geometry;
using Geometry.Transforms;
using Interfaces;
using System.Numerics;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    internal class Circle : IFigure
    {
        protected Point2d _center;
        protected double _radius;

        [DataMember]
        public Point2d Center => _center;
        [DataMember]
        public double Radius => _radius;

        public IEnumerable<IParameter<double>> DoubleParameters { get; protected set; }
        public IEnumerable<IParameter<Point2d>> PointParameters { get; protected set; }
        public IEnumerable<IParameter<Vector2d>> VectorParameters { get; protected set; }
        public Circle(Point2d center, double radius)
        {
            _center = center;
            _radius = radius;

            DoubleParameters = new List<IParameter<double>>();
            PointParameters = new List<IParameter<Point2d>>();
            VectorParameters = new List<IParameter<Vector2d>>();
        }

        public Circle(Circle circle)
        {
            _center = circle._center;
            _radius = circle._radius;

            DoubleParameters = new List<IParameter<double>>();
            PointParameters = new List<IParameter<Point2d>>();
            VectorParameters = new List<IParameter<Vector2d>>();

        }
        public virtual void Draw(IGraphics graphics)
        {
            graphics.DrawCircle(_center, _radius, false, true);
        }

        public bool IsInside(Vector2 p, float eps)
        {
            Vector2d v = _center;
            Vector2d dv = v - p;

            return eps >= 0 &&
                   Math.Abs(Math.Abs(dv.Norm) - _radius) > eps;
        }

        public void Rotate(float angle)
        {
            RotateTransform2D transform = new RotateTransform2D
            {
                Angle = angle,
                Center = _center
            };

        }
        public void Scale(float x, float y)
        {
            ScaleTransform2D transform = new ScaleTransform2D
            {
                ScaleX = x,
                ScaleY = y,
                Center = _center
            };
            _radius *= x;
        }
        public void Translate(Vector2 to)
        {
            _center += to;
        }

        public bool HasIntersection(IFigure figure)
        {
            throw new NotImplementedException();
        }

        public IFigure Intersect(IFigure second)
        {
            throw new NotImplementedException();
        }

        public IFigure Subtruct(IFigure second)
        {
            throw new NotImplementedException();
        }

        public IFigure Union(IFigure second)
        {
            throw new NotImplementedException();
        }

        public IFigure Clone()
        {
            return new Circle(this);
        }

        object ICloneable.Clone()
        {
            return new Circle(this);
        }

    }
    
}

