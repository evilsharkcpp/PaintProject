using System.Numerics;
using System.Runtime.InteropServices;

namespace DataStructures.Geometry
{
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public struct Vector2d
    {
        [FieldOffset(0)]
        private double _x;
        [FieldOffset(8)]
        private double _y;
        [FieldOffset(16)]
        private double _norm;

        public double X
        {
            get => _x;
            set
            {
                _x = value;
                _norm = -1;
            }
        }

        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                _norm = -1;
            }
        }

        public double Norm
        {
            get
            {
                if (_norm < 0)
                {
                    _norm = Math.Sqrt(X * X + Y * Y);
                }

                return _norm;
            }
        }

        public Vector2d(double x = 0, double y = 0)
        {
            _x = x;
            _y = y;
            _norm = -1;
        }


        public static Vector2d operator+(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2d operator+(Vector2 a, Vector2d b)
        {
            return new Vector2d(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2d operator+(Vector2d a, Vector2 b)
        {
            return new Vector2d(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2d operator-(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2d operator-(Vector2 a, Vector2d b)
        {
            return new Vector2d(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2d operator-(Vector2d a, Vector2 b)
        {
            return new Vector2d(a.X - b.X, a.Y - b.Y);
        }

        public static double operator*(Vector2d a, Vector2d b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static double operator*(Vector2 a, Vector2d b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static double operator*(Vector2d a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static double operator^(Vector2d a, Vector2d b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static double operator^(Vector2 a, Vector2d b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static double operator^(Vector2d a, Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static Vector2d operator*(double a, Vector2d b)
        {
            return new Vector2d(a * b.X, a * b.Y) { _norm = a * b._norm };
        }

        public static Vector2d operator*(Vector2d a, double b)
        {
            return new Vector2d(b * a.X, b * a.Y) { _norm = b * a._norm };
        }

        public static Vector2d operator/(Vector2d a, double b)
        {
            return new Vector2d(a.X / b, a.Y / b) { _norm = a._norm / b };
        }

        public static implicit operator Vector2(Vector2d a)
        {
            return new Vector2((float)a.X, (float)a.Y);
        }


        public void Sum(Vector2d a, ref Vector2d res)
        {
            res.X = _x + a.X;
            res.Y = _y + a.Y;
        }

        public void Sum(Vector2 a, ref Vector2d res)
        {
            res.X = _x + a.X;
            res.Y = _y + a.Y;
        }

        public void Sub(Vector2d a, ref Vector2d res)
        {
            res.X = _x - a.X;
            res.Y = _y - a.Y;
        }

        public void Sub(Vector2 a, ref Vector2d res)
        {
            res.X = _x - a.X;
            res.Y = _y - a.Y;
        }

        public void Normilize()
        {
            _x /= Norm;
            _y /= Norm;
            _norm = 1;
        }
    }
}
