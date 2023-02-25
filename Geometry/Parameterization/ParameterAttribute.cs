namespace Geometry.Parameterization
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class ParameterAttribute : Attribute
    {
        private readonly string _name;
        public string Name => _name;

        public ParameterAttribute(string name)
        {
            _name = name;
        }
    }
}