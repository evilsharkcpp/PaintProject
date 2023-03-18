
using DataStructures;

namespace Interfaces
{
    public interface IDrawable:ICloneable
    {
        bool IsNoFill { get; set; }
        bool IsOutLine { get; set; }
        Color FillColor { get; set; }
        Color OutLineColor { get; set; }
        double OutLineThickness { get; set; }
        new IDrawable Clone();
    }
}
