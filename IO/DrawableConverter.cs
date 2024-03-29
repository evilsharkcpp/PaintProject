﻿using Drawing.Graphics;
using Interfaces;
using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    public class DrawableConverter
    {
        public IDrawable getDrawable(SvgElement svg_elem)
        {
            IDrawable drawable = new Drawable();

            if (svg_elem.Fill != null)
            {
                if (svg_elem.Fill != new SvgColourServer(System.Drawing.Color.FromArgb(0, 0, 0, 0)))
                {
                    drawable.IsNoFill = true;
                    drawable.FillColor = new ColorConverter().getColor((SvgColourServer)svg_elem.Fill.Color);
                }
            }
            else
                drawable.IsNoFill = true;

            if (svg_elem.Stroke != null)
            {
                drawable.IsNoOutLine = false;
                drawable.OutLineColor = new ColorConverter().getColor((SvgColourServer)svg_elem.Stroke.Color);
                drawable.OutLineThickness = svg_elem.Stroke.StrokeWidth;

            }
            else
                drawable.IsNoOutLine = true;

            return drawable;
        }
    }
}
