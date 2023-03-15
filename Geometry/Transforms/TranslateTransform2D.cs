using DataStructures.Geometry;
using System.Numerics;

namespace Geometry.Transforms
{
    public class TranslateTransform2D : Transform2D
    {
        private Vector2d _v;
        public Vector2d V
        {
            get => _v;
            set
            {
                _v = value;
                _matrix.M13 = _v.X;
                _matrix.M23 = _v.Y;
            }
        }

        public TranslateTransform2D()
        {
            _v = new Vector2d(0, 0);
        }

        public override void Apply(Point2d p, ref Point2d res)
        {
            p.Sum(_v, ref res);
        }

        public override void Apply(Vector2d v, ref Vector2d res)
        {
            res = v;
        }

        public override void Apply(Vector2 v, ref Vector2d res)
        {
            res.X = v.X;
            res.Y = v.Y;
        }
    }
}
