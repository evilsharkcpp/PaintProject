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
            FileStream fs = new FileStream(fileName, FileMode.Open);
            XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            DataContractSerializer ser = new DataContractSerializer(typeof(Person));

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
