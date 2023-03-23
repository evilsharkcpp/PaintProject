using DataStructures.Geometry;
using System.Numerics;

namespace Geometry.Transforms
{
    public class RSTTransform2D : Transform2D
    {
        private double _sin = 0;
        private double _cos = 1;

        private double _angle = 0;
        private double _scaleX = 1;
        private double _scaleY = 1;
        private Vector2d _v = new Vector2d(0, 0);

        public double Angle
        {
            get => _angle;
            set
            {
                if (value != _angle)
                {
                    _angle = value;

                    _cos = Math.Cos(_angle);
                    _sin = Math.Sin(_angle);

                    _matrix.M11 = _scaleX * _cos;
                    _matrix.M12 = -_scaleY * _sin;

                    _matrix.M21 = _scaleX * _sin;
                    _matrix.M22 = _scaleY * _cos;
                }
            }
        }

        public double ScaleX
        {
            get => _scaleX;
            set
            {
                if (value != _scaleX && value > 0)
                {
                    _scaleX = value;

                    _matrix.M11 = _scaleX * _cos;
                    _matrix.M21 = _scaleX * _sin;

                    _matrix.M13 = _v.X + _scaleX;
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

                    _matrix.M12 = -_scaleY * _sin;
                    _matrix.M22 = _scaleY * _cos;

                    _matrix.M23 = _v.Y + _scaleY;
                }
            }
        }

        public Vector2d V
        {
            get => _v;
            set
            {
                _v = value;

                _matrix.M13 = _v.X + _scaleX;
                _matrix.M23 = _v.Y + _scaleY;
            }
        }


        public RSTTransform2D()
        {
            _matrix = new Matrix3d();
        }


        public override void Apply(Point2d p, ref Point2d res)
        {
            res.X = _matrix.M11 * p.X + _matrix.M12 * p.Y + _matrix.M13;
            res.Y = _matrix.M21 * p.X + _matrix.M22 * p.Y + _matrix.M23;
        }

        public override void Apply(Vector2d v, ref Vector2d res)
        {
            res.X = _matrix.M11 * v.X + _matrix.M12 * v.Y;
            res.Y = _matrix.M21 * v.X + _matrix.M22 * v.Y;
        }

        public override void Apply(Vector2 v, ref Vector2d res)
        {
            res.X = _matrix.M11 * v.X + _matrix.M12 * v.Y;
            res.Y = _matrix.M21 * v.X + _matrix.M22 * v.Y;
        }


        public void ApplyInv(Point2d p, ref Point2d res)
        {
            res.X = _cos / _scaleX * p.X + _sin / _scaleX * p.Y + (-_cos * (_v.X + _scaleX) - _sin * (_v.Y + _scaleY)) / _scaleX;
            res.Y = -_sin / _scaleY * p.X + _cos / _scaleY * p.Y + (_sin * (_v.X + _scaleX) - _cos * (_v.Y + _scaleY)) / _scaleY;
        }

        public void ApplyInv(Vector2d v, ref Vector2d res)
        {
            res.X = _cos / _scaleX * v.X + _sin / _scaleX * v.Y;
            res.Y = -_sin / _scaleY * v.X + _cos / _scaleY * v.Y;
        }

        public void ApplyInv(Vector2 v, ref Vector2d res)
        {
            res.X = _cos / _scaleX * v.X + _sin / _scaleX * v.Y;
            res.Y = -_sin / _scaleY * v.X + _cos / _scaleY * v.Y;
        }
    }
}
