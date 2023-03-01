namespace Interfaces
{
    public interface IParameter<T>
    {
        string Name { get; }
        T Value { get; set; }
    }
}
