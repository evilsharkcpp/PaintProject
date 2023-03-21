using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IConverter
    {
        IEnumerable<IDrawableObject> ReadFile(string filename);
        void WriteFile(string filename, IEnumerable<IDrawableObject> figures);
    }
}
