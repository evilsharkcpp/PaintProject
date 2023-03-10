using DataStructures.Geometry;
using Geometry.Parameterization;
using Geometry.Transforms;
using Interfaces;
using ReactiveUI;
using System.Numerics;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;


namespace Geometry.Figures
{
    [DataContract]
    [Figure("Circle")]
    internal class Circle : ParameterizedObject, IFigure
    {
        protected Point2d _center;
        protected double _radius;

        [DataMember]
        [Parameter("Center")]
        public Point2d Center
        {
            get => _center;
            set
            {
                this.RaiseAndSetIfChanged(ref _center, value);
            }
        }

        [DataMember]
        [Parameter("Radius")]
        public double Radius
        {
            get => _radius;
            set
            {
                this.RaiseAndSetIfChanged(ref _radius, value);
            }
        }

        //public IEnumerable<IParameter<double>> DoubleParameters { get; protected set; }
        //public IEnumerable<IParameter<Point2d>> PointParameters { get; protected set; }
        //public IEnumerable<IParameter<Vector2d>> VectorParameters { get; protected set; }
        public Circle() : this( new Point2d(), 0) { }
        public Circle(Point2d center, double radius)
        {
            _center = center;
            _radius = radius;
        }

        public Circle(Circle circle) : this(circle._center, circle._radius) { }
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

