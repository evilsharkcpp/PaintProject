using DataStructures.Geometry;

namespace Tests_xUnit_.Figures
{
    internal class Triangle : Figure
    {
        public Point2d V1;
        public Point2d V2;
        public Point2d V3;

        public override bool Equals(object? obj)
        {
            return Equals(obj as Triangle);
        }

        public bool Equals(Triangle? other)
        {
            return other is not null &&
                   V1.Equals(other.V1) &&
                   V2.Equals(other.V2) &&
                   V3.Equals(other.V3);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(V1, V2, V3);
        }

        public static bool operator ==(Triangle? left, Triangle? right)
        {
            return EqualityComparer<Triangle>.Default.Equals(left, right);
        }

        public static bool operator !=(Triangle? left, Triangle? right)
        {
            return !(left == right);
        }
    }
}
