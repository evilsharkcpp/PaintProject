﻿using System.Numerics;
using System.Runtime.InteropServices;

namespace DataStructures.Geometry
{
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct Point2d : IEquatable<Point2d>
    {
        [FieldOffset(0)]
        private double _x;
        [FieldOffset(8)]
        private double _y;

        public double X
        { 
            get => _x;
            set => _x = value;
        }

        public double Y
        {
            get => _y;
            set => _y = value;
        }

        public Point2d()
        {
            _x = 0;
            _y = 0;
        }

        public Point2d(double x = 0, double y = 0)
        {
            _x = x;
            _y = y;
        }


        public static Point2d operator +(Point2d a, Point2d b)
        {
            return new Point2d(a.X + b.X, a.Y + b.Y);
        }

        public static Point2d operator +(Point2d a, Vector2d b)
        {
            return new Point2d(a.X + b.X, a.Y + b.Y);
        }

        public static Point2d operator +(Point2d a, Vector2 b)
        {
            return new Point2d(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2d operator-(Point2d a, Point2d b)
        {
            return new Vector2d(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2d operator-(Vector2d a, Point2d b)
        {
            return new Vector2d(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2d operator-(Vector2 a, Point2d b)
        {
            return new Vector2d(a.X - b.X, a.Y - b.Y);
        }

        public static Point2d operator *(double a, Point2d b)
        {
            return new Vector2d(a * b._x, a * b._y);
        }

        public static Point2d operator *(Point2d a, double b)
        {
            return new Vector2d(b * a._x, b * a._y);
        }

        public static Point2d operator /(Point2d a, double b)
        {
            return new Vector2d(a._x / b, a._y / b);
        }

        public static Point2d operator +(Point2d a)
        {
            return new Point2d(a.X, a.Y);
        }

        public static Point2d operator -(Point2d a)
        {
            return new Point2d(-a.X, -a.Y);
        }

        public static bool operator ==(Point2d a, Point2d b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Point2d a, Point2d b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        public static bool operator >(Point2d a, Point2d b)
        {
            return a.X > b.X && a.Y > b.Y;
        }

        public static bool operator >=(Point2d a, Point2d b)
        {
            return a.X >= b.X && a.Y >= b.Y;
        }

        public static bool operator <(Point2d a, Point2d b)
        {
            return a.X < b.X && a.Y < b.Y;
        }

        public static bool operator <=(Point2d a, Point2d b)
        {
            return a.X <= b.X && a.Y <= b.Y;
        }

        public static implicit operator Point2d(Vector2d a)
        {
            return new Point2d(a.X, a.Y);
        }

        public static implicit operator Point2d(Vector2 a)
        {
            return new Point2d(a.X, a.Y);
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


        public override bool Equals(object? obj)
        {
            return obj is Point2d d && Equals(d);
        }

        public bool Equals(Point2d other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
