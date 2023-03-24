using DataStructures.Geometry;
using Geometry.Figures;
using Interfaces;
using ReactiveUI;
using System.Reactive.Linq;

namespace Logic.Utils
{
    internal class FigureBound : ReactiveObject, IFigureBound
    {
        private class BoundRectangle : Rectangle
        {
            public FigureChangeDirection FigureChangeDirection { get; private set; }

            protected override bool OnBound(Point2d p, double eps)
            {
                FigureChangeDirection = FigureChangeDirection.None;

                if (Math.Abs(Start.Y + Height - p.Y) <= eps)
                {
                    FigureChangeDirection |= FigureChangeDirection.Top;
                }

                if (Math.Abs(Start.X + Width - p.X) <= eps)
                {
                    FigureChangeDirection |= FigureChangeDirection.Right;
                }

                if (Math.Abs(Start.Y - p.Y) <= eps)
                {
                    FigureChangeDirection |= FigureChangeDirection.Bottom;
                }

                if (Math.Abs(Start.X - p.X) <= eps)
                {
                    FigureChangeDirection |= FigureChangeDirection.Left;
                }

                return FigureChangeDirection != FigureChangeDirection.None;
            }
        }

        private BoundRectangle _rectangle;

        private bool _isEmpty;
        private bool _isManySelect = false;

        private Point2d _position;
        private Vector2d _size;
        private IEnumerable<IFigure> _figures;
        private double _angle;
        private double padding = 5;

        public bool IsManySelect => _isManySelect;

        public Point2d Position
        {
            get => _position;
            private set
            {
                if (_position != value)
                {
                    this.RaiseAndSetIfChanged(ref _position, value);
                }
            }
        }

        public Vector2d Size
        {
            get => _size;
            private set
            {
                if (_size != value)
                {
                    this.RaiseAndSetIfChanged(ref _size, value);
                }
            }
        }

        public double Angle
        {
            get => _angle;
            private set
            {
                if (_angle != value)
                {
                    this.RaiseAndSetIfChanged(ref _angle, value);
                }
            }
        }

        public double Padding
        {
            get => padding;
            set
            {
                if (padding != value)
                {
                    double delta = value - padding;
                    Position += new Point2d(delta, delta);
                    Size += new Vector2d(2 * delta, 2 * delta);
                }
            }
        }

        public IEnumerable<IFigure?> Figures
        {
            get => _figures;
            set
            {
                int count = 0;
                foreach (IFigure? figure in value)
                {
                    if (count > 1)
                        break;

                    if (figure is not null)
                        count++;
                }
                _isEmpty = count == 0;
                _isManySelect = count > 1;

                Angle = 0;
                foreach (IFigure? f in _figures)
                {
                    if (f is not null)
                    {
                        f.WhenAnyValue(f => f.Position)
                         .Subscribe(position => UpdateBound(f));

                        f.WhenAnyValue(f => f.Size)
                         .Subscribe(size => UpdateBound(f));

                        f.WhenAnyValue(f => f.Angle)
                            .Subscribe(angle => UpdateBound(f));
                    }
                }
            }
        }


        public FigureBound()
        {
            _rectangle = new BoundRectangle();

            this.WhenAnyValue(fb => fb.Position)
                .Subscribe(position => _rectangle.Position = position);
            this.WhenAnyValue(fb => fb.Size)
                .Subscribe(size => _rectangle.Size = size);
            this.WhenAnyValue(fb => fb.Angle)
                .Subscribe(angle => _rectangle.Angle = angle);

            _figures = Array.Empty<IFigure>();
        }


        public bool OnBound(Point2d point, float eps)
        {
            return _rectangle.IsInside(point, eps);
        }

        public bool OnBound(Point2d point, float eps, out FigureChangeDirection direction)
        {
            bool successfully = _rectangle.IsInside(point, eps);

            direction = _rectangle.FigureChangeDirection;

            return successfully;
        }

        public void Translate(Vector2d delta, FigureChangeDirection direction)
        {
            if (!_isEmpty)
            {
                Vector2d v = new Vector2d(0, 0);
                Combine(delta, direction, ref v);

                foreach (IFigure figure in _figures)
                {
                    figure.Position += v;
                }
            }
        }

        public void Resize(Vector2d delta, FigureChangeDirection direction)
        {
            if (!_isEmpty)
            {
                Vector2d v = new Vector2d(0, 0);
                Combine(delta, direction, ref v);

                foreach (IFigure figure in _figures)
                {
                    figure.Size += v;
                }
            }
        }

        public void Rotate(Point2d start, Vector2d delta)
        {
            if (!_isEmpty)
            {
                Vector2d v1 = start, v2;
                v1.X -= Position.X + Size.X / 2.0;
                v1.Y -= Position.Y + Size.Y / 2.0;
                v2 = v1 + delta;

                double angle = Math.Acos(v2 * v1 / (v1.Norm * v2.Norm));
                if (!_isManySelect)
                {
                    IEnumerator<IFigure> enumerator = _figures.GetEnumerator();
                    if (enumerator.MoveNext())
                    {
                        enumerator.Current.Angle += angle;
                    }
                }
            }
        }

        public void Draw(IGraphics graphics)
        {
            _rectangle.Draw(graphics);
        }

        private void Combine(Vector2d delta, FigureChangeDirection direction, ref Vector2d v)
        {
            v.X = 0;
            v.Y = 0;

            if ((direction & FigureChangeDirection.Top) > 0)
            {
                v.Y += delta.Y;
            }

            if ((direction & FigureChangeDirection.Right) > 0)
            {
                v.X += delta.X;
            }

            if ((direction & FigureChangeDirection.Bottom) > 0)
            {
                v.Y -= delta.Y;
            }

            if ((direction & FigureChangeDirection.Left) > 0)
            {
                v.X -= delta.X;
            }
        }

        private void UpdateBound(IFigure figure)
        {
            if (_isManySelect)
            {
                double sin = Math.Sin(figure.Angle),
                       cos = Math.Cos(figure.Angle);
                Vector2d size = new Vector2d(figure.Size.X * Math.Abs(cos) + figure.Size.Y * Math.Abs(sin),
                                             figure.Size.X * Math.Abs(sin) + figure.Size.Y * Math.Abs(cos));
                Point2d position = figure.Position + (figure.Size - size) / 2.0;

                Point2d dPosition = position - Position;
                Position = new Point2d(dPosition.X < 0 ? Position.X : position.X + Padding,
                                       dPosition.Y < 0 ? Position.Y : position.Y + Padding);

                Size = new Vector2d(Math.Max(Size.X, dPosition.X + size.X + Padding),
                                    Math.Max(Size.Y, dPosition.Y + size.Y + Padding));
            }
            else
            {
                Position = figure.Position + new Point2d(Padding, Padding);
                Size = figure.Size + new Vector2d(2 * Padding, 2 * Padding);
                Angle = figure.Angle;
            }
        }
    }
}
