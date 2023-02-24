using System.Numerics;
using System.Runtime.InteropServices;

namespace DataStructures.Geometry
{
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct Point2d
    {
        [FieldOffset(0)]
        public double X;
        [FieldOffset(8)]
        public double Y;

        public Point2d(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }


        public static Point2d operator +(Point2d a, Vector2d b)
        {
            return new Point2d(a.X - b.X, a.Y - b.Y);
        }

        public static Point2d operator +(Point2d a, Vector2 b)
        {
            return new Point2d(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2d operator-(Point2d a, Point2d b)
        {
            return new Vector2d(a.X - b.X, a.Y - b.Y);
        }

        public static implicit operator Vector2d(Point2d a)
        {
            return new Vector2d(a.X, a.Y);
        }

        public static implicit operator Vector2(Point2d a)
        {
            return new Vector2((float)a.X, (float)a.Y);
        }


        public void Sum(Vector2d a, ref Point2d res)
        {
            res.X = X + a.X;
            res.Y = Y + a.Y;
        }

        public void Sum(Vector2 a, ref Point2d res)
        {
            res.X = X + a.X;
            res.Y = Y + a.Y;
        }

        public void Sub(Vector2d a, ref Point2d res)
        {
            res.X = X - a.X;
            res.Y = Y - a.Y;
        }

        public void Sub(Vector2 a, ref Point2d res)
        {
            res.X = X - a.X;
            res.Y = Y - a.Y;
        }
    }
}
