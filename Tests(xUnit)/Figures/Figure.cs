namespace Tests_xUnit_.Figures
{
    internal abstract class Figure
    {
        public bool IsFill { get; set; }
        public bool IsOutline { get; set; }

        public abstract override bool Equals(object? obj);
        public abstract override int GetHashCode();
    }
}
