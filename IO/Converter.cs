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
using System.Runtime.ConstrainedExecution;
using DynamicData;
using System.Runtime.Serialization;
using System.Xml;

namespace IO
{
    internal class JSONConverter : IConverter
    {
        public IEnumerable<IFigure> ReadFile(string filename)
        {
            IEnumerable<IFigure> deserializedFigures;

            FileStream fs = new FileStream(filename, FileMode.Open);
            var ser = new DataContractJsonSerializer(typeof(IEnumerable<IFigure>));

            deserializedFigures = (IEnumerable<IFigure>)ser.ReadObject(fs);
            fs.Close();

            return deserializedFigures;
        }

        public void WriteFile(string filename, IEnumerable<IFigure> figures)
        {
            FileStream stream = new FileStream(filename, FileMode.Create);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<IFigure>));
            ser.WriteObject(stream, figures);

        }

    }

    internal class SVGConverter : IConverter
    {
        public IEnumerable<IFigure> ReadFile(string filename)
        {
            throw new NotImplementedException();
        }

        public void WriteFile(string filename, IEnumerable<IFigure> figures)
        {


            foreach (IFigure figure in figures)
            {


            }

        }

    }
}
