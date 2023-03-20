using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures;

namespace Interfaces
{
    public interface IConverter
    {
        IEnumerable<(IFigure, IDrawable)> ReadFile(string filename);
        void WriteFile(string filename, IEnumerable<(IFigure, IDrawable)> figures);
    }
}
