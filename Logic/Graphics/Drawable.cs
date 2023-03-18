using Interfaces;
using System;
using DataStructures;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic.Graphics
{
    public class Drawable : IDrawable, INotifyPropertyChanged
    {
        public Color FillColor { get; set; }

        public Color OutLineColor { get; set; }

        public double OutLineThickness { get; set; }
        private bool _isNoFill = true;
        public bool IsNoFill
        {
            get => _isNoFill;
            set
            {
                _isNoFill = value;
                OnPropertyChanged();
            }
        }
        public bool IsOutLine { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public IDrawable Clone()
        {
            throw new NotImplementedException();
        }

        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }
        public Drawable(Color fill, Color stroke, double thickness = 1)
        {
            FillColor = fill;
            OutLineColor = stroke;
        }
        public Drawable()
        {
            FillColor = new Color(0, 255, 255, 255);
            OutLineColor = new Color(255, 0, 0, 0);
        }
    }
}
