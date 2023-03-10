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
using Geometry.Figures;
using ExCSS;

namespace IO
{
    public class JSONConverter : IConverter
    {
        public IEnumerable<IFigure> ReadFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            var ser = new DataContractJsonSerializer(typeof(IEnumerable<IFigure>), 
                        new Type[] {
                                typeof(Line),
                                typeof(Rectangle),
                                typeof(Triangle),
                                typeof(Square),
                            });

            IEnumerable<IFigure>? deserializedFigures = ser.ReadObject(fs) as IEnumerable<IFigure>;
            fs.Close();

            if (deserializedFigures != null)
                return deserializedFigures;
            else
                return Enumerable.Empty<IFigure>();
        }

        public void WriteFile(string filename, IEnumerable<IFigure> figures)
        {
            FileStream stream = new FileStream(filename, FileMode.Create);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IEnumerable<IFigure>),
                                                new Type[] { 
                                                    typeof(Line),
                                                    typeof(Rectangle),
                                                    typeof(Triangle),
                                                    typeof(Square),
                                                });
            ser.WriteObject(stream, figures);
            stream.Close();
        }

    }

    public class SVGConverter : IConverter
    {
        public IEnumerable<IFigure> ReadFile(string filename)
        {
            throw new NotImplementedException();
        }

        public void WriteFile(string filename, IEnumerable<IFigure> figures)
        {
            FileValidator.CheckParentDirectory(filename);

            foreach (IFigure figure in figures)
            {
                

            }

        }

    }
}
