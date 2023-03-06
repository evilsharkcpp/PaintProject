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
    [Figure("Square")]
    public class Square : ParameterizedObject, IFigure
    {
        private double _lenght;
        private Vector2d _v;

        protected Point2d _point1;
        protected Point2d _point2;
        protected ObservableAsPropertyHelper<Point2d> _center;

        private Point2d PointCorner(sbyte a, sbyte b)
        {
            double X = _center.Value.X + a * _v.X - b * _v.Y;
            double Y = _center.Value.Y + a * _v.Y - b * _v.X;
            return new Point2d((float)X, (float)Y);
        }

        private IEnumerable<Point2d> SquarePoints()
        {
            Point2d[] points = new Point2d[4]
            {
                PointCorner(-1,-1),
                PointCorner(1,-1),
                PointCorner(-1,1),
                PointCorner(1,1)
            };

            return points;
        }

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

        public float Width { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float Height { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float Angle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<Point2d> Points { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Square() : this(new Point2d(), new Point2d()) { }

        public Square(Point2d point1, Point2d point2)
        {
            _point1 = point1;
            _point2 = point2;

            _center = this.WhenAnyValue(figure => figure.Point1, figure => figure.Point2)
                          .Select<(Point2d point1, Point2d point2), Point2d>(t =>
                          {
                              return point1;
                          })
                          .ToProperty(this, figure => figure.Center);

            this.WhenAnyValue(figure => figure.Point1, figure => figure.Point2)
                .Subscribe(Observer.Create<(Point2d point1, Point2d point2)>(t =>
                {
                    _v.X = t.point2.X - t.point1.X;
                    _v.Y = t.point2.Y - t.point1.Y;
                    _lenght = _v.Norm;
                    _v.Normilize();
                }));
        }

        public Square(Square square) : this(square._point1, square._point2) { }

        public void Draw(IGraphics graphics)
        {
            graphics.DrawPolygon(SquarePoints(), true, false);
        }

        public bool IsInside(Vector2 p, float eps)
        {
            Vector2d u = new Vector2d()
            {
                X = _point2.X - p.X,
                Y = _point2.Y - p.Y
            };
            double l = u * _v,
                   h = u ^ _v;
            return Math.Abs(h) <= eps && l >= -eps && l - _lenght <= eps;
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

        public void Translate(Vector2 to)
        {
            _point1 += to;
            _point2 += to;
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
            return new Square(this);
        }

        object ICloneable.Clone()
        {
            return new Square(this);
        }
    }
}
