
using DataStructures;

namespace Interfaces
{
    public interface IDrawable:ICloneable
    {
        Color FillColor { get; set; }
        Color OutLineColor { get; set; }
        double OutLineThickness { get; set; }
        new IDrawable Clone();
    }
}
