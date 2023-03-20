using DataStructures.Geometry;

namespace Tests_xUnit_.Figures
{
    internal class Polygon : Figure
    {
        public IEnumerable<Point2d> Points { get; set; } = new List<Point2d>();

        public override bool Equals(object? obj)
        {
            return obj is Polygon polygon && Equals(polygon);
        }

        public bool Equals(Polygon other)
        {
            return EqualityComparer<IEnumerable<Point2d>>.Default.Equals(Points, other.Points);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Points);
        }

        public static bool operator ==(Polygon left, Polygon right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Polygon left, Polygon right)
        {
            return !(left == right);
        }
    }
}
