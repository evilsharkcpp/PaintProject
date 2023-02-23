using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IParameter<T>
    {
        string Name { get; }
        T Value { get; }
        bool TrySetParameter(string name, T value);
    }
}
