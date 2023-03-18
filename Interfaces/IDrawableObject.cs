using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDrawableObject
    {
        public IFigure? Figure { get; set; }
        public IDrawable? Drawable { get; set; }
    }
}
