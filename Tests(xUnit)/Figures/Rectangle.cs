using DataStructures.Geometry;

namespace Tests_xUnit_.Figures
{
    internal class Rectangle : Figure
    {
        public Point2d Start;
        public double a;
        public double b;

        public override bool Equals(object? obj)
        {
            return Equals(obj as Rectangle);
        }

        public bool Equals(Rectangle? other)
        {
            return other is not null &&
                   Start.Equals(other.Start) &&
                   a == other.a &&
                   b == other.b;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, a, b);
        }

        public static bool operator ==(Rectangle? left, Rectangle? right)
        {
            return EqualityComparer<Rectangle>.Default.Equals(left, right);
        }

        public static bool operator !=(Rectangle? left, Rectangle? right)
        {
            return !(left == right);
        }
    }
}
