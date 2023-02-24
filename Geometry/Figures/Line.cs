using DataStructures.Geometry;
using Geometry.Transforms;
using Interfaces;
using System.Numerics;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    public class Line : IFigure
    {
        protected Point2d _point1;
        protected Point2d _point2;
        protected Point2d _center;

        private void UpdateCenter()
        {
            _center.X = (_point1.X + _point2.X) / 2.0;
            _center.Y = (_point1.Y + _point2.Y) / 2.0;
        }

        [DataMember]
        public Point2d Point1
        {
            get => _point1;
            set
            {
                _point1 = value;
                UpdateCenter();
            }
        }

        [DataMember]
        public Point2d Point2
        {
            get => _point2;
            set
            {
                _point2 = value;
                UpdateCenter();
            }
        }

        public Point2d Center => _center;

        public IEnumerable<IParameter<double>> DoubleParameters { get; protected set; }
        public IEnumerable<IParameter<Point2d>> PointParameters { get; protected set; }
        public IEnumerable<IParameter<Vector2d>> VectorParameters { get; protected set; }

        public Line(Point2d point1, Point2d point2)
        {
            _point1 = point1;
            _point2 = point2;

            DoubleParameters = new List<IParameter<double>>();
            PointParameters = new List<IParameter<Point2d>>();
            VectorParameters = new List<IParameter<Vector2d>>();
        }

        public Line(Line line)
        {
            _point1 = line._point1;
            _point2 = line._point2;

            DoubleParameters = new List<IParameter<double>>();
            PointParameters = new List<IParameter<Point2d>>();
            VectorParameters = new List<IParameter<Vector2d>>();
        }

        public void Draw(IGraphics graphics)
        {
            graphics.DrawLine(_point1, _point2, true, false);
        }



        public bool IsInside(Vector2 p, float eps)
        {
            Vector2d v = _point2 - _point1;
            return eps >= 0 &&
                   Math.Abs(v.Y * p.X - v.X * p.Y + _point2.X * _point1.Y + _point1.Y * _point1.X) / v.Norm < eps;
        }

        public void Rotate(float angle)
        {
            RotateTransform2D transform = new RotateTransform2D
            {
                Angle = angle,
                Center = _center
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
                Center = _center
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
            return new Line(this);
        }

        object ICloneable.Clone()
        {
            return new Line(this);
        }
    }
}
