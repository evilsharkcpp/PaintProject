using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    public class ColorConverter
    {
        public DataStructures.Color getColor(SvgColourServer svg_color)
        {
            byte a = svg_color.Colour.A;
            byte r = svg_color.Colour.R;
            byte g = svg_color.Colour.G;
            byte b = svg_color.Colour.B;

            DataStructures.Color ds_color = new DataStructures.Color(a, r, g, b);

            return ds_color;
        }

        public SvgColourServer getSvgColor( DataStructures.Color ds_color)
        {
            int a = ds_color.A;
            int r = ds_color.R;
            int g = ds_color.G;
            int b = ds_color.B;

            SvgColourServer svg_color = new SvgColourServer(System.Drawing.Color.FromArgb(a,r,g,b));

            return svg_color;
        }
    }
}
