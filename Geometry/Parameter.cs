using DynamicData.Binding;
using Interfaces;
using System.ComponentModel;

namespace Geometry
{
    public class Parameter<T> : IParameter<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
            }
        }

        public string Name { get; protected init; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Parameter(string name)
        {
            Name = name;
        }
    }
}
