using System.Runtime.InteropServices;

namespace DataStructures.Geometry
{
    [StructLayout(LayoutKind.Explicit, Size = 72)]
    public struct Matrix3d
    {
        [FieldOffset(0)]
        public double M11;
        [FieldOffset(8)]
        public double M12;
        [FieldOffset(16)]
        public double M13;
        
        [FieldOffset(24)]
        public double M21;
        [FieldOffset(32)]
        public double M22;
        [FieldOffset(40)]
        public double M23;
        
        [FieldOffset(48)]
        public double M31;
        [FieldOffset(56)]
        public double M32;
        [FieldOffset(64)]
        public double M33;

        public Matrix3d()
        {
            M11 = 1;
            M12 = 0;
            M13 = 0;
            
            M21 = 0;
            M22 = 1;
            M23 = 0;
            
            M31 = 0;
            M32 = 0;
            M33 = 1;
        }


        public void Transpose()
        {
            (M12, M21) = (M21, M12);
            (M13, M31) = (M31, M13);

            (M23, M32) = (M32, M23);
        }


        public void ToFloat(float[] floats)
        {
            if (floats == null || floats.Length < 9)
                return;

            floats[0] = (float)M11;
            floats[1] = (float)M12;
            floats[2] = (float)M13;
            
            floats[3] = (float)M21;
            floats[4] = (float)M22;
            floats[5] = (float)M23;
            
            floats[6] = (float)M31;
            floats[7] = (float)M32;
            floats[8] = (float)M33;
        }

        public void ToFloatT(float[] floats)
        {
            if (floats == null || floats.Length < 9)
                return;

            floats[0] = (float)M11;
            floats[1] = (float)M21;
            floats[2] = (float)M31;
            
            floats[3] = (float)M12;
            floats[4] = (float)M22;
            floats[5] = (float)M32;
            
            floats[6] = (float)M13;
            floats[7] = (float)M23;
            floats[8] = (float)M33;
        }

        public void ToDouble(double[] doubles)
        {
            if (doubles == null || doubles.Length < 9)
                return;

            doubles[0] = M11;
            doubles[1] = M12;
            doubles[2] = M13;
            
            doubles[3] = M21;
            doubles[4] = M22;
            doubles[5] = M23;
            
            doubles[6] = M31;
            doubles[7] = M32;
            doubles[8] = M33;
        }

        public void ToDoubleT(double[] doubles)
        {
            if (doubles == null || doubles.Length < 9)
                return;

            doubles[0] = M11;
            doubles[1] = M21;
            doubles[2] = M31;
            
            doubles[3] = M12;
            doubles[4] = M22;
            doubles[5] = M32;
            
            doubles[6] = M13;
            doubles[7] = M23;
            doubles[8] = M33;
        }


        public void Product(Matrix3d mat, ref Matrix3d res)
        {
            res.M11 = M11 * mat.M11 + M12 * mat.M21 + M13 * mat.M31;
            res.M12 = M11 * mat.M12 + M12 * mat.M22 + M13 * mat.M32;
            res.M13 = M11 * mat.M13 + M12 * mat.M23 + M13 * mat.M33;

            res.M21 = M21 * mat.M11 + M22 * mat.M21 + M23 * mat.M31;
            res.M22 = M21 * mat.M12 + M22 * mat.M22 + M23 * mat.M32;
            res.M23 = M21 * mat.M13 + M22 * mat.M23 + M23 * mat.M33;

            res.M31 = M31 * mat.M11 + M32 * mat.M21 + M33 * mat.M31;
            res.M32 = M31 * mat.M12 + M32 * mat.M22 + M33 * mat.M32;
            res.M33 = M31 * mat.M13 + M32 * mat.M23 + M33 * mat.M33;
        }

        public void ProductT(Matrix3d mat, ref Matrix3d res)
        {
            res.M11 = M11 * mat.M11 + M12 * mat.M12 + M13 * mat.M13;
            res.M12 = M11 * mat.M21 + M12 * mat.M22 + M13 * mat.M23;
            res.M13 = M11 * mat.M31 + M12 * mat.M32 + M13 * mat.M33;

            res.M21 = M21 * mat.M11 + M22 * mat.M12 + M23 * mat.M13;
            res.M22 = M21 * mat.M21 + M22 * mat.M22 + M23 * mat.M23;
            res.M23 = M21 * mat.M31 + M22 * mat.M32 + M23 * mat.M33;

            res.M31 = M31 * mat.M11 + M32 * mat.M12 + M33 * mat.M13;
            res.M32 = M31 * mat.M21 + M32 * mat.M22 + M33 * mat.M23;
            res.M33 = M31 * mat.M31 + M32 * mat.M32 + M33 * mat.M33;
        }

        public void Product(Matrix3d mat, float[] res)
        {
            res[0] = (float)(M11 * mat.M11 + M12 * mat.M21 + M13 * mat.M31);
            res[1] = (float)(M11 * mat.M12 + M12 * mat.M22 + M13 * mat.M32);
            res[2] = (float)(M11 * mat.M13 + M12 * mat.M23 + M13 * mat.M33);

            res[3] = (float)(M21 * mat.M11 + M22 * mat.M21 + M23 * mat.M31);
            res[4] = (float)(M21 * mat.M12 + M22 * mat.M22 + M23 * mat.M32);
            res[5] = (float)(M21 * mat.M13 + M22 * mat.M23 + M23 * mat.M33);

            res[6] = (float)(M31 * mat.M11 + M32 * mat.M21 + M33 * mat.M31);
            res[7] = (float)(M31 * mat.M12 + M32 * mat.M22 + M33 * mat.M32);
            res[8] = (float)(M31 * mat.M13 + M32 * mat.M23 + M33 * mat.M33);
        }

        public void ProductT(Matrix3d mat, float[] res)
        {
            res[0] = (float)(M11 * mat.M11 + M12 * mat.M12 + M13 * mat.M13);
            res[1] = (float)(M11 * mat.M21 + M12 * mat.M22 + M13 * mat.M23);
            res[2] = (float)(M11 * mat.M31 + M12 * mat.M32 + M13 * mat.M33);

            res[3] = (float)(M21 * mat.M11 + M22 * mat.M12 + M23 * mat.M13);
            res[4] = (float)(M21 * mat.M21 + M22 * mat.M22 + M23 * mat.M23);
            res[5] = (float)(M21 * mat.M31 + M22 * mat.M32 + M23 * mat.M33);

            res[6] = (float)(M31 * mat.M11 + M32 * mat.M12 + M33 * mat.M13);
            res[7] = (float)(M31 * mat.M21 + M32 * mat.M22 + M33 * mat.M23);
            res[8] = (float)(M31 * mat.M31 + M32 * mat.M32 + M33 * mat.M33);
        }

        public void Product(Matrix3d mat, double[] res)
        {
            res[0] = M11 * mat.M11 + M12 * mat.M21 + M13 * mat.M31;
            res[1] = M11 * mat.M12 + M12 * mat.M22 + M13 * mat.M32;
            res[2] = M11 * mat.M13 + M12 * mat.M23 + M13 * mat.M33;

            res[3] = M21 * mat.M11 + M22 * mat.M21 + M23 * mat.M31;
            res[4] = M21 * mat.M12 + M22 * mat.M22 + M23 * mat.M32;
            res[5] = M21 * mat.M13 + M22 * mat.M23 + M23 * mat.M33;

            res[6] = M31 * mat.M11 + M32 * mat.M21 + M33 * mat.M31;
            res[7] = M31 * mat.M12 + M32 * mat.M22 + M33 * mat.M32;
            res[8] = M31 * mat.M13 + M32 * mat.M23 + M33 * mat.M33;
        }

        public void ProductT(Matrix3d mat, double[] res)
        {
            res[0] = M11 * mat.M11 + M12 * mat.M12 + M13 * mat.M13;
            res[1] = M11 * mat.M21 + M12 * mat.M22 + M13 * mat.M23;
            res[2] = M11 * mat.M31 + M12 * mat.M32 + M13 * mat.M33;

            res[3] = M21 * mat.M11 + M22 * mat.M12 + M23 * mat.M13;
            res[4] = M21 * mat.M21 + M22 * mat.M22 + M23 * mat.M23;
            res[5] = M21 * mat.M31 + M22 * mat.M32 + M23 * mat.M33;

            res[6] = M31 * mat.M11 + M32 * mat.M12 + M33 * mat.M13;
            res[7] = M31 * mat.M21 + M32 * mat.M22 + M33 * mat.M23;
            res[8] = M31 * mat.M31 + M32 * mat.M32 + M33 * mat.M33;
        }


        public void ReverseProduct(Matrix3d mat, ref Matrix3d res)
        {
            res.M11 = mat.M11 * M11 + mat.M12 * M21 + mat.M13 * M31;
            res.M12 = mat.M11 * M12 + mat.M12 * M22 + mat.M13 * M32;
            res.M13 = mat.M11 * M13 + mat.M12 * M23 + mat.M13 * M33;

            res.M21 = mat.M21 * M11 + mat.M22 * M21 + mat.M23 * M31;
            res.M22 = mat.M21 * M12 + mat.M22 * M22 + mat.M23 * M32;
            res.M23 = mat.M21 * M13 + mat.M22 * M23 + mat.M23 * M33;

            res.M31 = mat.M31 * M11 + mat.M32 * M21 + mat.M33 * M31;
            res.M32 = mat.M31 * M12 + mat.M32 * M22 + mat.M33 * M32;
            res.M33 = mat.M31 * M13 + mat.M32 * M23 + mat.M33 * M33;
        }

        public void ReverseProductT(Matrix3d mat, ref Matrix3d res)
        {
            res.M11 = mat.M11 * M11 + mat.M12 * M12 + mat.M13 * M13;
            res.M12 = mat.M11 * M21 + mat.M12 * M22 + mat.M13 * M23;
            res.M13 = mat.M11 * M31 + mat.M12 * M32 + mat.M13 * M33;

            res.M21 = mat.M21 * M11 + mat.M22 * M12 + mat.M23 * M13;
            res.M22 = mat.M21 * M21 + mat.M22 * M22 + mat.M23 * M23;
            res.M23 = mat.M21 * M31 + mat.M22 * M32 + mat.M23 * M33;

            res.M31 = mat.M31 * M11 + mat.M32 * M12 + mat.M33 * M13;
            res.M32 = mat.M31 * M21 + mat.M32 * M22 + mat.M33 * M23;
            res.M33 = mat.M31 * M31 + mat.M32 * M32 + mat.M33 * M33;
        }

        public void ReverseProduct(Matrix3d mat, float[] res)
        {
            res[0] = (float)(M11 * M11 + mat.M12 * M21 + mat.M13 * M31);
            res[1] = (float)(M11 * M12 + mat.M12 * M22 + mat.M13 * M32);
            res[2] = (float)(M11 * M13 + mat.M12 * M23 + mat.M13 * M33);

            res[3] = (float)(M21 * M11 + mat.M22 * M21 + mat.M23 * M31);
            res[4] = (float)(M21 * M12 + mat.M22 * M22 + mat.M23 * M32);
            res[5] = (float)(M21 * M13 + mat.M22 * M23 + mat.M23 * M33);

            res[6] = (float)(M31 * M11 + mat.M32 * M21 + mat.M33 * M31);
            res[7] = (float)(M31 * M12 + mat.M32 * M22 + mat.M33 * M32);
            res[8] = (float)(M31 * M13 + mat.M32 * M23 + mat.M33 * M33);
        }

        public void ReverseProductT(Matrix3d mat, float[] res)
        {
            res[0] = (float)(M11 * M11 + mat.M12 * M12 + mat.M13 * M13);
            res[1] = (float)(M11 * M21 + mat.M12 * M22 + mat.M13 * M23);
            res[2] = (float)(M11 * M31 + mat.M12 * M32 + mat.M13 * M33);

            res[3] = (float)(M21 * M11 + mat.M22 * M12 + mat.M23 * M13);
            res[4] = (float)(M21 * M21 + mat.M22 * M22 + mat.M23 * M23);
            res[5] = (float)(M21 * M31 + mat.M22 * M32 + mat.M23 * M33);

            res[6] = (float)(M31 * M11 + mat.M32 * M12 + mat.M33 * M13);
            res[7] = (float)(M31 * M21 + mat.M32 * M22 + mat.M33 * M23);
            res[8] = (float)(M31 * M31 + mat.M32 * M32 + mat.M33 * M33);
        }

        public void ReverseProduct(Matrix3d mat, double[] res)
        {
            res[0] = mat.M11 * M11 + mat.M12 * M21 + mat.M13 * M31;
            res[1] = mat.M11 * M12 + mat.M12 * M22 + mat.M13 * M32;
            res[2] = mat.M11 * M13 + mat.M12 * M23 + mat.M13 * M33;

            res[3] = mat.M21 * M11 + mat.M22 * M21 + mat.M23 * M31;
            res[4] = mat.M21 * M12 + mat.M22 * M22 + mat.M23 * M32;
            res[5] = mat.M21 * M13 + mat.M22 * M23 + mat.M23 * M33;

            res[6] = mat.M31 * M11 + mat.M32 * M21 + mat.M33 * M31;
            res[7] = mat.M31 * M12 + mat.M32 * M22 + mat.M33 * M32;
            res[8] = mat.M31 * M13 + mat.M32 * M23 + mat.M33 * M33;
        }

        public void ReverseProductT(Matrix3d mat, double[] res)
        {
            res[0] = mat.M11 * M11 + mat.M12 * M12 + mat.M13 * M13;
            res[1] = mat.M11 * M21 + mat.M12 * M22 + mat.M13 * M23;
            res[2] = mat.M11 * M31 + mat.M12 * M32 + mat.M13 * M33;

            res[3] = mat.M21 * M11 + mat.M22 * M12 + mat.M23 * M13;
            res[4] = mat.M21 * M21 + mat.M22 * M22 + mat.M23 * M23;
            res[5] = mat.M21 * M31 + mat.M22 * M32 + mat.M23 * M33;

            res[6] = mat.M31 * M11 + mat.M32 * M12 + mat.M33 * M13;
            res[7] = mat.M31 * M21 + mat.M32 * M22 + mat.M33 * M23;
            res[8] = mat.M31 * M31 + mat.M32 * M32 + mat.M33 * M33;
        }
    }
}
