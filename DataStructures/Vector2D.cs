using System.Reflection.Metadata.Ecma335;

namespace DataStructures
{
    public struct Vector2D
    {
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }
        public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);
    }
}