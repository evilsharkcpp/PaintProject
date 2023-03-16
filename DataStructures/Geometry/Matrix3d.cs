using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace DataStructures.Geometry
{
    [StructLayout(LayoutKind.Explicit, Size = 80)]
    public struct Matrix3d
    {
        [FieldOffset(0)]
        private double _M11;
        [FieldOffset(8)]
        private double _M12;
        [FieldOffset(16)]
        private double _M13;

        [FieldOffset(24)]
        private double _M21;
        [FieldOffset(32)]
        private double _M22;
        [FieldOffset(40)]
        private double _M23;

        [FieldOffset(48)]
        private double _M31;
        [FieldOffset(56)]
        private double _M32;
        [FieldOffset(64)]
        private double _M33;

        [FieldOffset(72)]
        private double _det;


        public double M11
        {
            get => _M11;
            set
            {
                _M11 = value;
                _det = double.NaN;
            }
        }
        public double M12
        {
            get => _M12;
            set
            {
                _M12 = value;
                _det = double.NaN;
            }
        }
        public double M13
        {
            get => _M13;
            set
            {
                _M13 = value;
                _det = double.NaN;
            }
        }

        public double M21
        {
            get => _M21;
            set
            {
                _M21 = value;
                _det = double.NaN;
            }
        }
        public double M22
        {
            get => _M22;
            set
            {
                _M22 = value;
                _det = double.NaN;
            }
        }
        public double M23
        {
            get => _M23;
            set
            {
                _M23 = value;
                _det = double.NaN;
            }
        }

        public double M31
        {
            get => _M31;
            set
            {
                _M31 = value;
                _det = double.NaN;
            }
        }
        public double M32
        {
            get => _M32;
            set
            {
                _M32 = value;
                _det = double.NaN;
            }
        }
        public double M33
        {
            get => _M33;
            set
            {
                _M33 = value;
                _det = double.NaN;
            }
        }

        public double Det
        {
            get
            {
                if (_det == double.NaN)
                    _det = _M11 * _M22 * _M33 +
                           _M12 * _M23 * _M31 +
                           _M13 * _M21 * _M32 -
                           _M13 * _M22 * _M31 -
                           _M11 * _M23 * _M32 -
                           _M12 * _M21 * _M33;

                return _det;
            }
        }

        public Matrix3d()
        {
            _M11 = 1;
            _M12 = 0;
            _M13 = 0;

            _M21 = 0;
            _M22 = 1;
            _M23 = 0;

            _M31 = 0;
            _M32 = 0;
            _M33 = 1;

            _det = double.NaN;
        }


        public void Transpose()
        {
            (_M12, _M21) = (_M21, _M12);
            (_M13, _M31) = (_M31, _M13);

            (_M23, _M32) = (_M32, _M23);
        }

        public void InverseFrom(Matrix3d mat)
        {
            _M11 = (mat._M22 * mat._M33 - mat._M23 * mat._M32) / Det;
            _M12 = (mat._M13 * mat._M32 - mat._M12 * mat._M33) / Det;
            _M13 = (mat._M12 * mat._M23 - mat._M13 * mat._M22) / Det;

            _M21 = (mat._M23 * mat._M31 - mat._M21 * mat._M33) / Det;
            _M22 = (mat._M11 * mat._M33 - mat._M13 * mat._M31) / Det;
            _M23 = (mat._M13 * mat._M21 - mat._M11 * mat._M23) / Det;

            _M31 = (mat._M21 * mat._M32 - mat._M22 * mat._M31) / Det;
            _M32 = (mat._M12 * mat._M31 - mat._M11 * mat._M32) / Det;
            _M33 = (mat._M11 * mat._M22 - mat._M12 * mat._M21) / Det;

            _det = double.NaN;
        }


        public void ToFloat(float[] floats)
        {
            if (floats == null || floats.Length < 9)
                return;

            floats[0] = (float)_M11;
            floats[1] = (float)_M12;
            floats[2] = (float)_M13;

            floats[3] = (float)_M21;
            floats[4] = (float)_M22;
            floats[5] = (float)_M23;

            floats[6] = (float)_M31;
            floats[7] = (float)_M32;
            floats[8] = (float)_M33;
        }

        public void ToFloatT(float[] floats)
        {
            if (floats == null || floats.Length < 9)
                return;

            floats[0] = (float)_M11;
            floats[1] = (float)_M21;
            floats[2] = (float)_M31;

            floats[3] = (float)_M12;
            floats[4] = (float)_M22;
            floats[5] = (float)_M32;

            floats[6] = (float)_M13;
            floats[7] = (float)_M23;
            floats[8] = (float)_M33;
        }

        public void ToDouble(double[] doubles)
        {
            if (doubles == null || doubles.Length < 9)
                return;

            doubles[0] = _M11;
            doubles[1] = _M12;
            doubles[2] = _M13;

            doubles[3] = _M21;
            doubles[4] = _M22;
            doubles[5] = _M23;

            doubles[6] = _M31;
            doubles[7] = _M32;
            doubles[8] = _M33;
        }

        public void ToDoubleT(double[] doubles)
        {
            if (doubles == null || doubles.Length < 9)
                return;

            doubles[0] = _M11;
            doubles[1] = _M21;
            doubles[2] = _M31;

            doubles[3] = _M12;
            doubles[4] = _M22;
            doubles[5] = _M32;

            doubles[6] = _M13;
            doubles[7] = _M23;
            doubles[8] = _M33;
        }


        public void Product(Matrix3d mat, ref Matrix3d res)
        {
            res._M11 = _M11 * mat._M11 + _M12 * mat._M21 + _M13 * mat._M31;
            res._M12 = _M11 * mat._M12 + _M12 * mat._M22 + _M13 * mat._M32;
            res._M13 = _M11 * mat._M13 + _M12 * mat._M23 + _M13 * mat._M33;

            res._M21 = _M21 * mat._M11 + _M22 * mat._M21 + _M23 * mat._M31;
            res._M22 = _M21 * mat._M12 + _M22 * mat._M22 + _M23 * mat._M32;
            res._M23 = _M21 * mat._M13 + _M22 * mat._M23 + _M23 * mat._M33;

            res._M31 = _M31 * mat._M11 + _M32 * mat._M21 + _M33 * mat._M31;
            res._M32 = _M31 * mat._M12 + _M32 * mat._M22 + _M33 * mat._M32;
            res._M33 = _M31 * mat._M13 + _M32 * mat._M23 + _M33 * mat._M33;

            res._det = mat._det * _det;
        }

        public void ProductT(Matrix3d mat, ref Matrix3d res)
        {
            res._M11 = _M11 * mat._M11 + _M12 * mat._M12 + _M13 * mat._M13;
            res._M12 = _M11 * mat._M21 + _M12 * mat._M22 + _M13 * mat._M23;
            res._M13 = _M11 * mat._M31 + _M12 * mat._M32 + _M13 * mat._M33;

            res._M21 = _M21 * mat._M11 + _M22 * mat._M12 + _M23 * mat._M13;
            res._M22 = _M21 * mat._M21 + _M22 * mat._M22 + _M23 * mat._M23;
            res._M23 = _M21 * mat._M31 + _M22 * mat._M32 + _M23 * mat._M33;

            res._M31 = _M31 * mat._M11 + _M32 * mat._M12 + _M33 * mat._M13;
            res._M32 = _M31 * mat._M21 + _M32 * mat._M22 + _M33 * mat._M23;
            res._M33 = _M31 * mat._M31 + _M32 * mat._M32 + _M33 * mat._M33;

            res._det = mat._det * _det;
        }

        public void Product(Matrix3d mat, float[] res)
        {
            res[0] = (float)(_M11 * mat._M11 + _M12 * mat._M21 + _M13 * mat._M31);
            res[1] = (float)(_M11 * mat._M12 + _M12 * mat._M22 + _M13 * mat._M32);
            res[2] = (float)(_M11 * mat._M13 + _M12 * mat._M23 + _M13 * mat._M33);

            res[3] = (float)(_M21 * mat._M11 + _M22 * mat._M21 + _M23 * mat._M31);
            res[4] = (float)(_M21 * mat._M12 + _M22 * mat._M22 + _M23 * mat._M32);
            res[5] = (float)(_M21 * mat._M13 + _M22 * mat._M23 + _M23 * mat._M33);

            res[6] = (float)(_M31 * mat._M11 + _M32 * mat._M21 + _M33 * mat._M31);
            res[7] = (float)(_M31 * mat._M12 + _M32 * mat._M22 + _M33 * mat._M32);
            res[8] = (float)(_M31 * mat._M13 + _M32 * mat._M23 + _M33 * mat._M33);
        }

        public void ProductT(Matrix3d mat, float[] res)
        {
            res[0] = (float)(_M11 * mat._M11 + _M12 * mat._M12 + _M13 * mat._M13);
            res[1] = (float)(_M11 * mat._M21 + _M12 * mat._M22 + _M13 * mat._M23);
            res[2] = (float)(_M11 * mat._M31 + _M12 * mat._M32 + _M13 * mat._M33);

            res[3] = (float)(_M21 * mat._M11 + _M22 * mat._M12 + _M23 * mat._M13);
            res[4] = (float)(_M21 * mat._M21 + _M22 * mat._M22 + _M23 * mat._M23);
            res[5] = (float)(_M21 * mat._M31 + _M22 * mat._M32 + _M23 * mat._M33);

            res[6] = (float)(_M31 * mat._M11 + _M32 * mat._M12 + _M33 * mat._M13);
            res[7] = (float)(_M31 * mat._M21 + _M32 * mat._M22 + _M33 * mat._M23);
            res[8] = (float)(_M31 * mat._M31 + _M32 * mat._M32 + _M33 * mat._M33);
        }

        public void Product(Matrix3d mat, double[] res)
        {
            res[0] = _M11 * mat._M11 + _M12 * mat._M21 + _M13 * mat._M31;
            res[1] = _M11 * mat._M12 + _M12 * mat._M22 + _M13 * mat._M32;
            res[2] = _M11 * mat._M13 + _M12 * mat._M23 + _M13 * mat._M33;

            res[3] = _M21 * mat._M11 + _M22 * mat._M21 + _M23 * mat._M31;
            res[4] = _M21 * mat._M12 + _M22 * mat._M22 + _M23 * mat._M32;
            res[5] = _M21 * mat._M13 + _M22 * mat._M23 + _M23 * mat._M33;

            res[6] = _M31 * mat._M11 + _M32 * mat._M21 + _M33 * mat._M31;
            res[7] = _M31 * mat._M12 + _M32 * mat._M22 + _M33 * mat._M32;
            res[8] = _M31 * mat._M13 + _M32 * mat._M23 + _M33 * mat._M33;
        }

        public void ProductT(Matrix3d mat, double[] res)
        {
            res[0] = _M11 * mat._M11 + _M12 * mat._M12 + _M13 * mat._M13;
            res[1] = _M11 * mat._M21 + _M12 * mat._M22 + _M13 * mat._M23;
            res[2] = _M11 * mat._M31 + _M12 * mat._M32 + _M13 * mat._M33;

            res[3] = _M21 * mat._M11 + _M22 * mat._M12 + _M23 * mat._M13;
            res[4] = _M21 * mat._M21 + _M22 * mat._M22 + _M23 * mat._M23;
            res[5] = _M21 * mat._M31 + _M22 * mat._M32 + _M23 * mat._M33;

            res[6] = _M31 * mat._M11 + _M32 * mat._M12 + _M33 * mat._M13;
            res[7] = _M31 * mat._M21 + _M32 * mat._M22 + _M33 * mat._M23;
            res[8] = _M31 * mat._M31 + _M32 * mat._M32 + _M33 * mat._M33;
        }


        public void Product(Point2d v, ref Point2d res)
        {
            double w = _M31 * v.X + _M32 * v.Y + _M33;
            res.X = (_M11 * v.X + _M12 * v.Y + _M13) / w;
            res.Y = (_M21 * v.X + _M22 * v.Y + _M23) / w;
        }

        public void ProductT(Point2d v, ref Point2d res)
        {
            double w = _M13 * v.X + _M23 * v.Y + _M33;
            res.X = (_M11 * v.X + _M21 * v.Y + _M31) / w;
            res.Y = (_M12 * v.X + _M22 * v.Y + _M32) / w;
        }

        public void Product(Vector2d v, ref Vector2d res)
        {
            res.X = _M11 * v.X + _M12 * v.Y;
            res.Y = _M21 * v.X + _M22 * v.Y;
        }

        public void ProductT(Vector2d v, ref Vector2d res)
        {
            res.X = _M11 * v.X + _M21 * v.Y;
            res.Y = _M12 * v.X + _M22 * v.Y;
        }

        public void Product(Vector2 v, ref Vector2d res)
        {
            res.X = _M11 * v.X + _M12 * v.Y;
            res.Y = _M21 * v.X + _M22 * v.Y;
        }

        public void ProductT(Vector2 v, ref Vector2d res)
        {
            res.X = _M11 * v.X + _M21 * v.Y;
            res.Y = _M12 * v.X + _M22 * v.Y;
        }

        public void Product(Vector2d v, ref Vector2 res)
        {
            res.X = (float)(_M11 * v.X + _M12 * v.Y);
            res.Y = (float)(_M21 * v.X + _M22 * v.Y);
        }

        public void ProductT(Vector2d v, ref Vector2 res)
        {
            res.X = (float)(_M11 * v.X + _M21 * v.Y);
            res.Y = (float)(_M12 * v.X + _M22 * v.Y);
        }

        public void Product(Vector2 v, ref Vector2 res)
        {
            res.X = (float)(_M11 * v.X + _M12 * v.Y);
            res.Y = (float)(_M21 * v.X + _M22 * v.Y);
        }

        public void ProductT(Vector2 v, ref Vector2 res)
        {
            res.X = (float)(_M11 * v.X + _M21 * v.Y);
            res.Y = (float)(_M12 * v.X + _M22 * v.Y);
        }


        public void ReverseProduct(Matrix3d mat, ref Matrix3d res)
        {
            res._M11 = mat._M11 * _M11 + mat._M12 * _M21 + mat._M13 * _M31;
            res._M12 = mat._M11 * _M12 + mat._M12 * _M22 + mat._M13 * _M32;
            res._M13 = mat._M11 * _M13 + mat._M12 * _M23 + mat._M13 * _M33;

            res._M21 = mat._M21 * _M11 + mat._M22 * _M21 + mat._M23 * _M31;
            res._M22 = mat._M21 * _M12 + mat._M22 * _M22 + mat._M23 * _M32;
            res._M23 = mat._M21 * _M13 + mat._M22 * _M23 + mat._M23 * _M33;

            res._M31 = mat._M31 * _M11 + mat._M32 * _M21 + mat._M33 * _M31;
            res._M32 = mat._M31 * _M12 + mat._M32 * _M22 + mat._M33 * _M32;
            res._M33 = mat._M31 * _M13 + mat._M32 * _M23 + mat._M33 * _M33;

            res._det = mat._det * _det;
        }

        public void ReverseProductT(Matrix3d mat, ref Matrix3d res)
        {
            res._M11 = mat._M11 * _M11 + mat._M12 * _M12 + mat._M13 * _M13;
            res._M12 = mat._M11 * _M21 + mat._M12 * _M22 + mat._M13 * _M23;
            res._M13 = mat._M11 * _M31 + mat._M12 * _M32 + mat._M13 * _M33;

            res._M21 = mat._M21 * _M11 + mat._M22 * _M12 + mat._M23 * _M13;
            res._M22 = mat._M21 * _M21 + mat._M22 * _M22 + mat._M23 * _M23;
            res._M23 = mat._M21 * _M31 + mat._M22 * _M32 + mat._M23 * _M33;

            res._M31 = mat._M31 * _M11 + mat._M32 * _M12 + mat._M33 * _M13;
            res._M32 = mat._M31 * _M21 + mat._M32 * _M22 + mat._M33 * _M23;
            res._M33 = mat._M31 * _M31 + mat._M32 * _M32 + mat._M33 * _M33;

            res._det = mat._det * _det;
        }

        public void ReverseProduct(Matrix3d mat, float[] res)
        {
            res[0] = (float)(_M11 * _M11 + mat._M12 * _M21 + mat._M13 * _M31);
            res[1] = (float)(_M11 * _M12 + mat._M12 * _M22 + mat._M13 * _M32);
            res[2] = (float)(_M11 * _M13 + mat._M12 * _M23 + mat._M13 * _M33);

            res[3] = (float)(_M21 * _M11 + mat._M22 * _M21 + mat._M23 * _M31);
            res[4] = (float)(_M21 * _M12 + mat._M22 * _M22 + mat._M23 * _M32);
            res[5] = (float)(_M21 * _M13 + mat._M22 * _M23 + mat._M23 * _M33);

            res[6] = (float)(_M31 * _M11 + mat._M32 * _M21 + mat._M33 * _M31);
            res[7] = (float)(_M31 * _M12 + mat._M32 * _M22 + mat._M33 * _M32);
            res[8] = (float)(_M31 * _M13 + mat._M32 * _M23 + mat._M33 * _M33);
        }

        public void ReverseProductT(Matrix3d mat, float[] res)
        {
            res[0] = (float)(_M11 * _M11 + mat._M12 * _M12 + mat._M13 * _M13);
            res[1] = (float)(_M11 * _M21 + mat._M12 * _M22 + mat._M13 * _M23);
            res[2] = (float)(_M11 * _M31 + mat._M12 * _M32 + mat._M13 * _M33);

            res[3] = (float)(_M21 * _M11 + mat._M22 * _M12 + mat._M23 * _M13);
            res[4] = (float)(_M21 * _M21 + mat._M22 * _M22 + mat._M23 * _M23);
            res[5] = (float)(_M21 * _M31 + mat._M22 * _M32 + mat._M23 * _M33);

            res[6] = (float)(_M31 * _M11 + mat._M32 * _M12 + mat._M33 * _M13);
            res[7] = (float)(_M31 * _M21 + mat._M32 * _M22 + mat._M33 * _M23);
            res[8] = (float)(_M31 * _M31 + mat._M32 * _M32 + mat._M33 * _M33);
        }

        public void ReverseProduct(Matrix3d mat, double[] res)
        {
            res[0] = mat._M11 * _M11 + mat._M12 * _M21 + mat._M13 * _M31;
            res[1] = mat._M11 * _M12 + mat._M12 * _M22 + mat._M13 * _M32;
            res[2] = mat._M11 * _M13 + mat._M12 * _M23 + mat._M13 * _M33;

            res[3] = mat._M21 * _M11 + mat._M22 * _M21 + mat._M23 * _M31;
            res[4] = mat._M21 * _M12 + mat._M22 * _M22 + mat._M23 * _M32;
            res[5] = mat._M21 * _M13 + mat._M22 * _M23 + mat._M23 * _M33;

            res[6] = mat._M31 * _M11 + mat._M32 * _M21 + mat._M33 * _M31;
            res[7] = mat._M31 * _M12 + mat._M32 * _M22 + mat._M33 * _M32;
            res[8] = mat._M31 * _M13 + mat._M32 * _M23 + mat._M33 * _M33;
        }

        public void ReverseProductT(Matrix3d mat, double[] res)
        {
            res[0] = mat._M11 * _M11 + mat._M12 * _M12 + mat._M13 * _M13;
            res[1] = mat._M11 * _M21 + mat._M12 * _M22 + mat._M13 * _M23;
            res[2] = mat._M11 * _M31 + mat._M12 * _M32 + mat._M13 * _M33;

            res[3] = mat._M21 * _M11 + mat._M22 * _M12 + mat._M23 * _M13;
            res[4] = mat._M21 * _M21 + mat._M22 * _M22 + mat._M23 * _M23;
            res[5] = mat._M21 * _M31 + mat._M22 * _M32 + mat._M23 * _M33;

            res[6] = mat._M31 * _M11 + mat._M32 * _M12 + mat._M33 * _M13;
            res[7] = mat._M31 * _M21 + mat._M32 * _M22 + mat._M33 * _M23;
            res[8] = mat._M31 * _M31 + mat._M32 * _M32 + mat._M33 * _M33;
        }
    }
}
