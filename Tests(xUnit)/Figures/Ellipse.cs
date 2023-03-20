using DataStructures.Geometry;

namespace Tests_xUnit_.Figures
{
    internal class Ellipse : Figure
    {
        public Point2d Start;
        public double a;
        public double b;

        public override bool Equals(object? obj)
        {
            return Equals(obj as Ellipse);
        }

        public bool Equals(Ellipse? other)
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

        public static bool operator ==(Ellipse? left, Ellipse? right)
        {
            return EqualityComparer<Ellipse>.Default.Equals(left, right);
        }

        public static bool operator !=(Ellipse? left, Ellipse? right)
        {
            return !(left == right);
        }
    }
}
