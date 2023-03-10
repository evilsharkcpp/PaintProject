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
    [Figure("Ellipse")]
    internal class Ellipse : ParameterizedObject, IFigure
    {
        private Vector2d _v;

        protected Point2d _point1; 
        protected Point2d _point2;
        protected ObservableAsPropertyHelper<Point2d> _center;
        protected double _a;
        protected double _b;


        [DataMember]
        [Parameter("Point1")]
        public Point2d Point1
        {
            get => _point1;
            set
            {
                this.RaiseAndSetIfChanged(ref _point1, value);
            }
        }

        [DataMember]
        [Parameter("Point2")]
        public Point2d Point2
        {
            get => _point2;
            set
            {
                this.RaiseAndSetIfChanged(ref _point2, value);
            }
        }
        public Point2d Center => _center.Value;

        public Ellipse() : this(new Point2d(), new Point2d()) { }

        public Ellipse(Point2d point1, Point2d point2)
        {
            _point1 = point1;
            _point2 = point2;

            _center = this.WhenAnyValue(figure => figure.Point1, figure => figure.Point2)
                          .Select<(Point2d point1, Point2d point2), Point2d>(t =>
                          {
                              return new Point2d((t.point1.X + t.point2.X) / 2.0, (t.point1.Y + t.point2.Y) / 2.0);
                          })
                          .ToProperty(this, figure => figure.Center);

            _a = Math.Abs(point1.X - point2.X) / 2;
            _b = Math.Abs(point1.Y - point2.Y) / 2;

        }
        public Ellipse(Ellipse ellipse) : this(ellipse._point1, ellipse._point2) { }
        public IFigure Clone()
        {
            throw new NotImplementedException();
        }

        public void Draw(IGraphics graphics)
        {
            graphics.DrawEllipse(_center.Value, _a, _b, false, true);
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
            return eps >= 0 &&
                   Math.Abs(p.X * p.X / (_a * _a) + p.Y * p.Y / (_b * _b) - 1) <= eps;
        }

        public void Rotate(float angle)
        {
            //RotateTransform2D transform = new RotateTransform2D
            //{
            //    Angle = angle,
            //    Center = Center
            //};

            //transform.Apply(_point1, ref _point1);
            //transform.Apply(_point2, ref _point2);
        }

        public void Scale(float x, float y)
        {
            ScaleTransform2D transform = new ScaleTransform2D
            {
                ScaleX = x,
                ScaleY = y,
                Center = Center
            };

            transform.Apply(_point1, ref _point1);
            transform.Apply(_point2, ref _point2);
        }

        public IFigure Subtruct(IFigure second)
        {
            throw new NotImplementedException();
        }

        public void Translate(Vector2 to)
        {
            _point1 += to;
            _point2 += to;
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
