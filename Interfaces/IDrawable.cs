using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDrawable:ICloneable
    {
        Vector3 FillColor { get; }
        Vector3 OutLineColor { get; }
        double OutLineThickness { get; }
        new IDrawable Clone();
    }
}
