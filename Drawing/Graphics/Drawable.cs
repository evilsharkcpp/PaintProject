using Interfaces;
using System;
using DataStructures;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace Drawing.Graphics
{
    [DataContract(Name = "Drawable")]

    public class Drawable : IDrawable, INotifyPropertyChanged
    {
        [DataMember(Name = "FillColor")]
        public Color FillColor { get; set; }

        [DataMember(Name = "OutLineColor")]
        public Color OutLineColor { get; set; }

        [DataMember(Name = "OutLineThickness")]
        public double OutLineThickness { get; set; }

        private bool _isNoFill = true;

        [DataMember(Name = "IsNoFill")]
        public bool IsNoFill
        {
            get => _isNoFill;
            set
            {
                _isNoFill = value;
                OnPropertyChanged();
            }
        }
        private bool _isNoOutLine = false;

        [DataMember(Name = "IsNoOutLine")]
        public bool IsNoOutLine
        {
            get => _isNoOutLine;
            set
            {
                _isNoOutLine = value;
                OnPropertyChanged();
            }
        }

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
            OutLineThickness = thickness;
        }
        public Drawable()
        {
            FillColor = new Color(0, 255, 255, 255);
            OutLineColor = new Color(255, 0, 0, 0);
            OutLineThickness = 1;
        }
    }
}
