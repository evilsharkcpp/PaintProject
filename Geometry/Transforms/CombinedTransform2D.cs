namespace Geometry.Transforms
{
    public class CombinedTransform2D : Transform2D
    {
        public CombinedTransform2D() { }

        public CombinedTransform2D Combine(Transform2D transform)
        {
            transform.Matrix.ReverseProduct(_matrix, ref _matrix);
            return this;
        }
    }
}
