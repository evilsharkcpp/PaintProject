namespace Interfaces
{
    public interface IDrawableObject
    {
        public IFigure? Figure { get; set; }
        public IDrawable? Drawable { get; set; }
    }
}
