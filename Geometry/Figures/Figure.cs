﻿using DataStructures.Geometry;
using Geometry.Parameterization;
using Geometry.Transforms;
using Interfaces;
using System.Numerics;

namespace Geometry.Figures
{
    public abstract class Figure : ParameterizedObject, IFigure
    {
        protected bool _bindSize = false;

        private readonly IReadOnlyList<INode> _nodes;
        public IReadOnlyList<INode> Nodes => _nodes;

        private RSTTransform2D _transform;
        private double _angle = 0;
        private Vector2d _size = new Vector2d(2, 2);
        private Point2d _position = new Point2d(0, 0);

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


        public Figure()
        {
            _transform = new RSTTransform2D();

            _nodes = new List<INode>();
        }

        public Figure(Figure figure)
        {
            _transform = new RSTTransform2D();

            Angle = figure.Angle;
            Size = figure.Size;
            Position = figure.Position;

            _nodes = new List<INode>();
        }


        public void Draw(IGraphics graphics)
        {
            graphics.ModelMatrix = _transform.Matrix;
            OnDraw(graphics);
        }

        public bool IsInside(Vector2 p, float eps)
        {
            Vector2d v = new Vector2d();
            _transform.ApplyInv(p, ref v);
            return IsInside(new Point2d(v.X, v.Y), eps / Math.Max(Size.X, Size.Y));
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
        protected abstract bool InArea(Rect rect, double eps);

        protected abstract Path ToPath();
    }
}
