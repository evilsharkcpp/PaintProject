namespace Geometry.Attributes
{
    internal class FigureAttribute : Attribute
    {
        public string Name { get; private set; }

        public FigureAttribute(string name)
        {
            Name = name;
        }
    }
}