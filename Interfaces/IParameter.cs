using System.ComponentModel;

namespace Interfaces
{
    public interface IParameter<T> : INotifyPropertyChanged
    {
        string Name { get; }
        T Value { get; set; }
    }
}
