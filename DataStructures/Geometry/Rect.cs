namespace DataStructures.Geometry
{
    public struct Rect
    {
        public Point2d Start = new Point2d(0, 0);
        public Point2d End = new Point2d(0, 0);

        public Rect(Point2d start, Point2d end)
        {
            Start = start;
            End = end;
        }
    }
}
