using System.Numerics;
using System.Runtime.InteropServices;

namespace DataStructures.Geometry
{
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public struct Vector2d : IEquatable<Vector2d>
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

        public Vector2d()
        {
            _x = 0;
            _y = 0;
            _norm = 0;
        }

        public Vector2d(double x = 0, double y = 0)
        {
            _x = x;
            _y = y;
            _norm = -1;
        }


        public static Vector2d operator+(Vector2d a, Vector2d b)
        {
            return new Vector2d(a._x + b._x, a._y + b._y);
        }

        public static Vector2d operator+(Vector2 a, Vector2d b)
        {
            return new Vector2d(a.X + b._x, a.Y + b._y);
        }

        public static Vector2d operator+(Vector2d a, Vector2 b)
        {
            return new Vector2d(a._x + b.X, a._y + b.Y);
        }

        public static Vector2d operator-(Vector2d a, Vector2d b)
        {
            return new Vector2d(a._x - b._x, a._y - b._y);
        }

        public static Vector2d operator-(Vector2 a, Vector2d b)
        {
            return new Vector2d(a.X - b._x, a.Y - b._y);
        }

        public static Vector2d operator-(Vector2d a, Vector2 b)
        {
            return new Vector2d(a._x - b.X, a._y - b.Y);
        }

        public static double operator*(Vector2d a, Vector2d b)
        {
            return a._x * b._x + a._y * b._y;
        }

        public static double operator*(Vector2 a, Vector2d b)
        {
            return a.X * b._x + a.Y * b._y;
        }

        public static double operator*(Vector2d a, Vector2 b)
        {
            return a._x * b.X + a._y * b.Y;
        }

        public static double operator^(Vector2d a, Vector2d b)
        {
            return a._x * b._y - a._y * b._x;
        }

        public static double operator^(Vector2 a, Vector2d b)
        {
            return a.X * b._y - a.Y * b._x;
        }

        public static double operator^(Vector2d a, Vector2 b)
        {
            return a._x * b.Y - a._y * b.X;
        }

        public static Vector2d operator*(double a, Vector2d b)
        {
            return new Vector2d(a * b._x, a * b._y) { _norm = a * b._norm };
        }

        public static Vector2d operator*(Vector2d a, double b)
        {
            return new Vector2d(b * a._x, b * a._y) { _norm = b * a._norm };
        }

        public static Vector2d operator/(Vector2d a, double b)
        {
            return new Vector2d(a._x / b, a._y / b) { _norm = a._norm / b };
        }

        public static Vector2d operator+(Vector2d a)
        {
            return new Vector2d(a._x, a._y);
        }

        public static Vector2d operator-(Vector2d a)
        {
            return new Vector2d(-a._x, -a._y);
        }

        public static bool operator==(Vector2d a, Vector2d b)
        {
            return a._x == b._x && a._y == b._y;
        }

        public static bool operator!=(Vector2d a, Vector2d b)
        {
            return a._x != b._x || a._y != b._y;
        }

        public static implicit operator Vector2(Vector2d a)
        {
            return new Vector2((float)a._x, (float)a._y);
        }

        public static implicit operator Vector2d(Vector2 a)
        {
            return new Vector2(a.X, a.X);
        }


        public void Sum(Vector2d a, ref Vector2d res)
        {
            res._x = _x + a._x;
            res._y = _y + a._y;
        }

        public void Sum(Vector2 a, ref Vector2d res)
        {
            res._x = _x + a.X;
            res._y = _y + a.Y;
        }

        public void Sub(Vector2d a, ref Vector2d res)
        {
            res._x = _x - a._x;
            res._y = _y - a._y;
        }

        public void Sub(Vector2 a, ref Vector2d res)
        {
            res._x = _x - a.X;
            res._y = _y - a.Y;
        }

        public void Normilize()
        {
            _x /= Norm;
            _y /= Norm;
            _norm = 1;
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector2d d && Equals(d);
        }

        public bool Equals(Vector2d other)
        {
            return _x == other._x && _y == other._y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_x, _y);
        }
    }
}
