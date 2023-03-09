using DataStructures.Geometry;
using System.Numerics;

namespace Geometry.Transforms
{
    public class ScaleTransform2D : Transform2D
    {
        private double _scaleX;
        private double _scaleY;
        private Point2d _center;

        public double ScaleX
        {
            get => _scaleX;
            set
            {
                if (value != _scaleX && value > 0)
                {
                    _scaleX = value;
                    _matrix.M11 = _scaleX;
                    _matrix.M13 = _center.X * (_scaleX - 1);
                }
            }
        }

        public double ScaleY
        {
            get => _scaleY;
            set
            {
                if (value != _scaleY && value > 0)
                {
                    _scaleY = value;
                    _matrix.M22 = _scaleY;
                    _matrix.M23 = _center.Y * (_scaleY - 1);
                }
            }
        }

        public Point2d Center
        {
            get => _center;
            set
            {
                if (value != _center)
                {
                    _center = value;
                    _matrix.M13 = _center.X * (_scaleX - 1);
                    _matrix.M23 = _center.Y * (_scaleY - 1);
                }
            }
        }


        public ScaleTransform2D()
        {
            _scaleX = 1;
            _scaleY = 1;
            _center = new Point2d(0, 0);
        }


        public override void Apply(Point2d p, ref Point2d res)
        {
            res.X = _matrix.M11 * p.X + _matrix.M13;
            res.Y = _matrix.M22 * p.Y + _matrix.M23;
        }

        public override void Apply(Vector2d v, ref Vector2d res)
        {
            res.X = _matrix.M11 * v.X;
            res.Y = _matrix.M22 * v.Y;
        }

        public override void Apply(Vector2 v, ref Vector2d res)
        {
            res.X = _matrix.M11 * v.X;
            res.Y = _matrix.M22 * v.Y;
        }
    }
}
