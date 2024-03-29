﻿using DataStructures;
using DataStructures.ConvertibleFigures;
using DataStructures.Geometry;
using Geometry.Attributes;
using Interfaces;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    [Figure("Ellipse")]
    public class Ellipse : Figure
    {
        protected static Point2d Center = new Point2d(0, 0);
        protected static double Radius = 1;

        public Ellipse() { }

        public Ellipse(Ellipse ellipse) : base(ellipse) { }

        protected override void OnDraw(IGraphics graphics)
        {
            graphics.DrawEllipse(Center, Radius, Radius, false, true);
        }

        protected override bool IsInside(Point2d p, double eps)
        {
            return Radius * Radius - (p.X * p.X + p.Y * p.Y) >= -eps * eps;
        }

        protected override bool OnBound(Point2d p, double eps)
        {
            return Math.Abs(Radius * Radius - (p.X * p.X + p.Y * p.Y)) >= -eps * eps;
        }

        public override IFigure Clone()
        {
            return new Ellipse(this);
        }

        protected override Path ToPath()
        {
            throw new NotImplementedException();
        }

        public override ConvertibleFigure ToConvertibleFigure()
        {
            Point2d center = new Point2d(Position.X + Size.X / 2, Position.Y + Size.Y / 2);

            return new ConvertibleEllipse(center, Size.X / 2, Size.Y / 2, Angle);
        }
    }
}