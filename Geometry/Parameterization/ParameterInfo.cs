using System.Reflection;

namespace Geometry.Parameterization
{
    internal class ParameterInfo
    {
        private readonly string _name;
        private readonly MethodInfo? _getMethod;
        private readonly MethodInfo? _setMethod;

        public string Name => _name;
        public MethodInfo? GetMethod => _getMethod;
        public MethodInfo? SetMethod => _setMethod;

        public ParameterInfo(string name, MethodInfo? getMethod, MethodInfo? setMethod)
        {
            _name = name;
            _getMethod = getMethod;
            _setMethod = setMethod;
        }
    }
}
