using System.Reflection;

namespace Geometry.Parameterization
{
    internal class ParameterInfo
    {
        private readonly string _name;
        private readonly Type _type;
        private readonly MethodInfo? _getMethod;
        private readonly MethodInfo? _setMethod;

        public string Name => _name;
        public Type Type => _type;
        public MethodInfo? GetMethod => _getMethod;
        public MethodInfo? SetMethod => _setMethod;

        public ParameterInfo(string name, Type type, MethodInfo? getMethod, MethodInfo? setMethod)
        {
            _name = name;
            _type = type;
            _getMethod = getMethod;
            _setMethod = setMethod;
        }
    }
}
