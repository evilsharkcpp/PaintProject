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
    [DataContract]
    [Figure("FilledCircle")]
    internal class FilledCircle : Circle
    {
        public FilledCircle(Circle circle) : base(circle) {}

        public FilledCircle(Point2d center, double radius) : base(center, radius) {}

        public override void Draw(IGraphics graphics)
        {
            graphics.DrawCircle(_center, _radius, true, true);
        }
    }
}
