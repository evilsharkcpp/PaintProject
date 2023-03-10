using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


using Interfaces;
using System.Runtime.Serialization.Json;
using Splat.ModeDetection;

namespace IO
{
    internal class ConvertToJSON : IConverter
    {
        public IEnumerable<IFigure> ReadFile(string filename)
        {
            throw new NotImplementedException();
        }

        public void WriteFile(string filename, IEnumerable<IFigure> figures)
        {
            FileStream stream = new FileStream(filename, FileMode.Create);

            foreach (IFigure figure in figures)
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IFigure));
                ser.WriteObject(stream, figure);
            }
        }

    }
}
