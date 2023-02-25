using Interfaces;

namespace Geometry
{
    public class Parameter<T> : IParameter<T>
    {
        private readonly Func<T> _getter;
        private readonly Action<T> _setter;

        public T Value
        {
            get => _getter();
            set => _setter(value);
        }

        public string Name { get; protected init; }

        public Parameter(string name, Func<T> getter, Action<T> setter)
        {
            Name = name;
            _getter = getter;
            _setter = setter;
        }
    }
}