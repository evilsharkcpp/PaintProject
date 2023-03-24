using DataStructures;
using DataStructures.Geometry;
using Geometry.Parameterization;
using Geometry.Transforms;
using Interfaces;
using ReactiveUI;
using System.Numerics;

namespace Geometry.Figures
{
    public abstract class Figure : ParameterizedObject, IFigure
    {
        protected bool _bindSize = false;

        private readonly IReadOnlyList<INode> _nodes;
        private readonly IReadOnlyList<(string, ReactiveCommand<Point2d, bool>)> _commands;

        private RSTTransform2D _transform;
        private double _angle = 0;
        private Vector2d _size = new Vector2d(2, 2);
        private Point2d _position = new Point2d(0, 0);


        public IReadOnlyList<INode> Nodes => _nodes;
        public IReadOnlyList<(string, ReactiveCommand<Point2d, bool>)> Commands => _commands;

        public double Angle
        {
            get => _angle;
            set
            {
                if (_angle != value)
                {
                    _angle = value;
                    _transform.Angle = _angle;
                }
            }
        }

        public Vector2d Size
        {
            get => _size;
            set
            {
                _size = value;

                if (_bindSize)
                {
                    double max = Math.Max(_size.X, _size.Y) / 2;
                    _transform.ScaleX = max;
                    _transform.ScaleY = max;
                }
                else
                {
                    _transform.ScaleX = _size.X / 2.0;
                    _transform.ScaleY = _size.Y / 2.0;
                }
            }
        }

        public Point2d Position
        {
            get => _position;
            set
            {
                _position.X = value.X + 1;
                _position.Y = value.Y + 1;
                _transform.V = _position;
            }
        }

        public Matrix3d ModelMatrix => _transform.Matrix;


        public Figure()
        {
            _transform = new RSTTransform2D();

            _nodes = new List<INode>();
            _commands = new List<(string, ReactiveCommand<Point2d, bool>)>();
        }

        public Figure(Figure figure)
        {
            _transform = new RSTTransform2D();

            Angle = figure.Angle;
            Size = figure.Size;
            Position = figure.Position;

            _nodes = new List<INode>();
            _commands = new List<(string, ReactiveCommand<Point2d, bool>)>();
        }


        public void Draw(IGraphics graphics)
        {
            graphics.ModelMatrix = _transform.Matrix;
            OnDraw(graphics);
        }

        public bool IsInside(Vector2 p, float eps)
        {
            Point2d v = new Point2d();
            _transform.ApplyInv((Point2d)p, ref v);
            return IsInside(v, eps / Math.Max(Size.X, Size.Y));
        }

        public bool OnBound(Vector2 p, float eps)
        {
            Point2d v = new Point2d();
            _transform.ApplyInv((Point2d)p, ref v);
            return OnBound(v, eps / Math.Max(Size.X, Size.Y));
        }

        public bool InArea(Rect rect, float eps)
        {
            Point2d p1 = new Point2d(), p2 = new Point2d();
            _transform.ApplyInv(rect.Start, ref p1);
            _transform.ApplyInv(rect.End, ref p2);
            return InArea(new Rect(p1, p2), eps / Math.Max(Size.X, Size.Y));
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


        public abstract IFigure Clone();
        object ICloneable.Clone()
        {
            return Clone();
        }


        protected abstract void OnDraw(IGraphics graphics);
        protected abstract bool IsInside(Point2d p, double eps);
        protected abstract bool OnBound(Point2d p, double eps);
        protected abstract bool InArea(Rect rect, double eps);

        protected abstract Path ToPath();

        public abstract ConvertibleFigure ToConvertibleFigure();
    }
}
