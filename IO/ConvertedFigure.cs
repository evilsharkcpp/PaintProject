using DataStructures.Geometry;
using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IO
{
    [DataContract(Name = "ConvertedFigure")]
    public class ConvertedFigure
    {

        public ConvertedFigure()
        {

        }

        public SvgElement toSVG(double x1, double y1, double x2, double y2)
        {
            return null;
        }
    }
}
