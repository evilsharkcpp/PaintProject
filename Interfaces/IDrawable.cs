
using DataStructures;

namespace Interfaces
{
    public interface IDrawable:ICloneable
    {
        bool IsNoFill { get; set; }
        bool IsNoOutLine { get; set; }
        Color FillColor { get; set; }
        Color OutLineColor { get; set; }
        double OutLineThickness { get; set; }
        new IDrawable Clone();
    }
}
