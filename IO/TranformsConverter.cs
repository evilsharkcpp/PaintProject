using DataStructures;
using IO.SVGFigures;
using Drawing.Graphics;
using Microsoft.VisualBasic;
using Svg;
using Svg.Transforms;


namespace IO
{
    public class TranformsConverter
    {
        public double getAngle(SvgElement svg_elem)
        {
            double angle = 0;

            if (svg_elem.Transforms != null)
            {
                foreach (var t in svg_elem.Transforms)
                    if (t.GetType() == typeof(SvgRotate))
                    {
                        var rotate = t as SvgRotate;
                        angle = rotate.Angle;
                    }

            }

            return angle;

        }

        public SvgTransformCollection getSvgTransforms(ConvertibleFigure cf)
        {
            SvgTransformCollection transforms = new SvgTransformCollection();

            double cx = cf.position.X + (cf.Width  / 2 );
            double cy = cf.position.Y + (cf.Height / 2);
            double angle = cf.angle;

            SvgRotate rotate = new SvgRotate(angle: (float)angle, centerX: (float)cx, centerY: (float)cy);

            transforms.Add(rotate);

            return transforms;
        }
    }
}
