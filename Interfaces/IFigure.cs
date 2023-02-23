using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IFigure:ICloneable,INotifyPropertyChanged
    {
        bool HasIntersection(IFigure figure);
        void Rotate(float angle);
        void Translate(Vector2 to);
        void Scale(float x, float y);
        new IFigure Clone();
        IFigure Intersect(IFigure second);
        IFigure Union(IFigure second);
        IFigure Subtruct(IFigure second);
        void Draw(IGraphics graphics);
        bool IsInside(Vector2 p, float eps);
        IEnumerable<IParameter<float>> FloatParameters { get; }
        IEnumerable<IParameter<Vector2>> Vector2Parameters { get; }
    }
}
