﻿using DataStructures.ConvertibleFigures;
using DataStructures;
using DataStructures.Geometry;
using Geometry.Attributes;
using Interfaces;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    [Figure("Path")]
    public class Path : Figure
    {
        public override IFigure Clone()
        {
            throw new NotImplementedException();
        }

        protected override bool IsInside(Point2d p, double eps)
        {
            throw new NotImplementedException();
        }

        protected override bool OnBound(Point2d p, double eps)
        {
            throw new NotImplementedException();
        }

        protected override void OnDraw(IGraphics graphics)
        {
            throw new NotImplementedException();
        }

        protected override Path ToPath()
        {
            throw new NotImplementedException();
        }

        public override ConvertibleFigure ToConvertibleFigure()
        {
            throw new NotImplementedException();
        }
    }
}
