using Interfaces;

namespace Geometry.Parameterization
{
    public delegate T Getter<T>();
    public delegate void Setter<T>(T value);

    internal class Parameter<ValueType> : IParameter<ValueType>
    {
        private readonly Getter<ValueType>? _getter;
        private readonly Setter<ValueType>? _setter;

        private readonly bool _hasGetter;
        private readonly bool _hasSetter;

        public bool HasGetter => _hasGetter;
        public bool HasSetter => _hasSetter;

        public ValueType Value
        {
            get
            {
                if (_hasGetter)
                {
                    return _getter!();
                }
                else
                {
                    throw new Exception();
                }
            }
            set
            {
                if (_hasGetter)
                {
                    _setter!(value);
                }
            }
        }

        public string Name { get; protected init; }

        public Parameter(string name, Getter<ValueType>? getter, Setter<ValueType>? setter)
        {
            Name = name;
            _getter = getter;
            _setter = setter;

            _hasGetter = _setter != null;
            _hasSetter = _getter != null;
        }
    }
}