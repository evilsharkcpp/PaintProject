using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IParameter<T>:INotifyPropertyChanged
    {
        string Name { get; }
        T Value { get; set; }
    }
}
