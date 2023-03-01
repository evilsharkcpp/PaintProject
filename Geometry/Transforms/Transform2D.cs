using DataStructures.Geometry;

namespace Geometry.Transforms
{
    public class Transform2D
    {
        protected Matrix3d _matrix;
        public Matrix3d Matrix => _matrix;

        public Transform2D()
        {
            _matrix = new Matrix3d();
        }


        public virtual void Apply(Point2d p, ref Point2d res)
        {
            double w = _matrix.M31 * p.X + _matrix.M32 * p.Y + _matrix.M33;
            res.X = (_matrix.M11 * p.X + _matrix.M12 * p.Y + _matrix.M13) / w;
            res.Y = (_matrix.M21 * p.X + _matrix.M22 * p.Y + _matrix.M23) / w;
        }

        public virtual void Apply(Vector2d v, ref Vector2d res)
        {
            double w = _matrix.M31 * v.X + _matrix.M32 * v.Y;
            res.X = (_matrix.M11 * v.X + _matrix.M12 * v.Y) / w;
            res.Y = (_matrix.M21 * v.X + _matrix.M22 * v.Y) / w;
        }
    }
}
