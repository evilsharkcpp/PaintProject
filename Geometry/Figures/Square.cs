using DataStructures.Geometry;
using Geometry.Parameterization;
using Geometry.Transforms;
using Interfaces;
using ReactiveUI;
using System.Numerics;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    public class Square : Rectangle
    {
        public Square() : base() {}
        public Square(Point2d point1, Point2d point2) : base(point1, point2) { }

        public Square(Square square) : this(square._point1, square._point2) { }

    }
}
