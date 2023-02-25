using DataStructures.Geometry;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry.Figures
{
    internal class FilledCircle : Circle
    {
        public FilledCircle(Circle circle) : base(circle)
        {
        }

        public FilledCircle(Point2d center, double radius) : base(center, radius)
        {
        }

        public override void Draw(IGraphics graphics)
        {

            graphics.DrawCircle(_center, _radius, true, true);
        }
    }
}
