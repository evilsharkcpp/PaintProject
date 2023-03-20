using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataStructures
{
    [DataContract(Name = "Color")]
    public class Color
    {
        [DataMember(Name = "A")]
        public byte A { get; set; }

        [DataMember(Name = "R")]
        public byte R { get; set; }

        [DataMember(Name = "G")]
        public byte G { get; set; }

        [DataMember(Name = "B")]
        public byte B { get; set; }


        public Color(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }
    }
}
