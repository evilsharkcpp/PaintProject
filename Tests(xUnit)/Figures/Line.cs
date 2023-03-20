using DataStructures.Geometry;

namespace Tests_xUnit_.Figures
{
    internal class Line : Figure
    {
        public Point2d V1;
        public Point2d V2;

        public override bool Equals(object? obj)
        {
            return obj is Line line && Equals(line);
        }

        public bool Equals(Line other)
        {
            return V1.Equals(other.V1) &&
                   V2.Equals(other.V2);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(V1, V2);
        }

        public static bool operator ==(Line left, Line right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Line left, Line right)
        {
            return !(left == right);
        }
    }
}
