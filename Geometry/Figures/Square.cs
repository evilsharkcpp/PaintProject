using DataStructures.Geometry;
using Geometry.Parameterization;
using Geometry.Transforms;
using Interfaces;
using ReactiveUI;
using System.Numerics;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace Geometry.Figures
{
    [DataContract]
    [Figure("Square")]
    public class Square : Rectangle
    {
        private protected new double l1=1;
        private protected new double l2=1;
    }
}
