using Interfaces;
using System;
using DataStructures;

namespace GUI_WPF.Graphics
{
    public class Drawable : IDrawable
    {
        public Color FillColor { get; set; }

        public Color OutLineColor { get; set; }

        public double OutLineThickness { get; set; }

        public IDrawable Clone()
        {
            throw new NotImplementedException();
        }

        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }
        public Drawable(Color fill, Color stroke, double thickness = 1)
        {
            FillColor  = fill;
            OutLineColor = stroke; 
        }
        public Drawable()
        {
            FillColor = new Color(0, 0, 0, 0);
            OutLineColor = new Color(255, 0, 0, 0);
        }
    }
}
