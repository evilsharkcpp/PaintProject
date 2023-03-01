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
    [Figure("Triangle")]
    public class Triangle : ParameterizedObject, IFigure
    {
        private Vector2d _v1;
        private Vector2d _v2;
        private Vector2d _v3;

        protected Point2d _point1;
        protected Point2d _point2;
        protected Point2d _point3;
        protected ObservableAsPropertyHelper<Point2d> _center;

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

        [DataMember]
        [Parameter("Point3")]
        public Point2d Point3
        {
            get => _point3;
            set
            {
                this.RaiseAndSetIfChanged(ref _point3, value);
            }
        }

        public Point2d Center => _center.Value;

        public Triangle() : this(new Point2d(), new Point2d(), new Point2d()) { }

        public Triangle(Point2d point1, Point2d point2, Point2d point3)
        {
            _point1 = point1;
            _point2 = point2;
            _point3 = point3;

            _center = this.WhenAnyValue(figure => figure.Point1, figure => figure.Point2, figure => figure.Point3)
                          .Select<(Point2d point1, Point2d point2, Point2d point3), Point2d>(t =>
                          {
                              return new Point2d((t.point1.X + t.point2.X + t.point3.X) / 3.0, (t.point1.Y + t.point2.Y + t.point3.Y) / 3.0);
                          })
                          .ToProperty(this, figure => figure.Center);

            this.WhenAnyValue(figure => figure.Point1, figure => figure.Point2, figure => figure.Point3)
                .Subscribe(Observer.Create<(Point2d point1, Point2d point2, Point2d point3)>(t =>
                {
                    _v1.X = t.point2.X - t.point1.X;
                    _v1.Y = t.point2.Y - t.point1.Y;
                    _v1.Normilize();

                    _v2.X = t.point3.X - t.point2.X;
                    _v2.Y = t.point3.Y - t.point2.Y;
                    _v2.Normilize();

                    _v3.X = t.point1.X - t.point3.X;
                    _v3.Y = t.point1.Y - t.point3.Y;
                    _v3.Normilize();
                }));
        }

        public Triangle(Triangle triangle) : this(triangle._point1, triangle._point2, triangle._point3) { }

        public void Draw(IGraphics graphics)
        {
            graphics.DrawTriangle(_point1, _point2, _point3, false, true);
        }



        public bool IsInside(Vector2 p, float eps)
        {
            double h1 = (p - _point1) ^ _v1,
                   h2 = (p - _point2) ^ _v2,
                   h3 = (p - _point3) ^ _v3;
            return h1 >= -eps && h2 >= -eps && h3 >= -eps ||
                   h1 <= eps && h2 <= eps && h3 >= eps;
        }

        public void Rotate(float angle)
        {
            RotateTransform2D transform = new RotateTransform2D
            {
                Angle = angle,
                Center = Center
            };

            transform.Apply(_point1, ref _point1);
            transform.Apply(_point2, ref _point2);
            transform.Apply(_point3, ref _point3);
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
            transform.Apply(_point3, ref _point3);
        }

        public void Translate(Vector2 to)
        {
            _point1 += to;
            _point2 += to;
            _point3 += to;
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
            return new Triangle(this);
        }

        object ICloneable.Clone()
        {
            return new Triangle(this);
        }
    }
}
