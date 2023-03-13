using DataStructures.Geometry;
using Geometry.Parameterization;
using Interfaces;
using System.Numerics;

namespace Geometry.Figures
{
    internal class Node : INode
    {
        private readonly IParameter<Point2d> _parameter;

        public void Change(Vector2 delta)
        {
            _parameter.Value += delta;
        }

        public Node(string name, Getter<Point2d>? getter, Setter<Point2d>? setter)
        {
            _parameter = new Parameter<Point2d>(name, getter, setter);
        }
    }
}
