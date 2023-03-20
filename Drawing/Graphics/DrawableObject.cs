using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Graphics
{
    public class DrawableObject:IDrawableObject
    {
        public IFigure? Figure { get; set; }
        public IDrawable? Drawable { get; set; }
        public DrawableObject() { }
        public DrawableObject(IFigure figure, IDrawable drawable)
        {
            Figure = figure;
            Drawable = drawable;
        }
    }
}
