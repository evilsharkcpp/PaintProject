using DataStructures.Geometry;
using System.Numerics;

namespace Geometry.Transforms
{
    public class RotateTransform2D : Transform2D
    {
        private double _angle;
        private Point2d _center;

        public double Angle
        {
            get => _angle;
            set
            {
                if (value != _angle)
                {
                    _angle = value;
                    _matrix.M11 = Math.Cos(_angle);
                    _matrix.M21 = Math.Sin(_angle);

                    _matrix.M12 = -_matrix.M21;
                    _matrix.M13 = _center.X * (_matrix.M11 - 1) - _matrix.M21 * _center.Y;

                    _matrix.M22 = _matrix.M11;
                    _matrix.M23 = _center.Y * (_matrix.M11 - 1) + _matrix.M21 * _center.X;
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
                    _matrix.M13 = _center.X * (_matrix.M11 - 1) - _matrix.M21 * _center.Y;
                    _matrix.M23 = _center.Y * (_matrix.M11 - 1) + _matrix.M21 * _center.X;
                }
            }
        }

        public RotateTransform2D()
        {
            _angle = 0;
            _center = new Point2d(0, 0);
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
    }
}
