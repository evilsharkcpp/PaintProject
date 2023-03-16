﻿using System.Runtime.Serialization;
using DataStructures.Geometry;

namespace DataStructures.СonvertibleFigures
{
    [DataContract(Name = "Ellips")]
    public class СonvertibleEllips : ConvertibleFigure
    {
        // Координаты центра
        [DataMember(Name = "Center")]
        public Point2d center;

        // Длина радиуса по оси X
        [DataMember(Name = "RadiusX")]
        public double radiusX;

        // Длина радиуса по оси Y
        [DataMember(Name = "RadiusY")]
        public double radiusY;

        // Цвет границы эллипса
        [DataMember(Name = "Color")]
        public Color color;

        public СonvertibleEllips(Point2d center, double radiusX, double radiusY, Color color)
        {
            this.center = center;
            this.radiusX = radiusX;
            this.radiusY = radiusY;
            this.color = color;
        }
    }
}
