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
using Svg;
using System.Xml.Linq;
using IO.SVGFigures;
using System.Data.SqlTypes;
using System.IO;

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
            FileStream stream = new FileStream(filename + ".json", FileMode.Create);

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
            SvgDocument svg_doc = new SvgDocument{ Width = 500, Height = 500};

            foreach (IFigure figure in figures)
            {
                
                switch (figure.GetType().Name)
                {
                    case "Line":
                        double x1 = figure.PointParameters.ElementAt(0).Value.X;
                        double y1 = figure.PointParameters.ElementAt(0).Value.Y;
                        double x2 = figure.PointParameters.ElementAt(1).Value.X;
                        double y2 = figure.PointParameters.ElementAt(1).Value.Y;


                        var line = new SVGLine().Line(x1, y1, x2, y2);
                        svg_doc.Children.Add(line);
                        break;

                    case "Rectangle":

                        break;

                    case "Triangle":

                        break;

                    case "Square":

                        break;

                    case "Cicle":

                        break;

                    case "Ellips":

                        break;

                    case "FilledCicrle":

                        break;
                }

                MemoryStream stream = new MemoryStream();
                svg_doc.Write(stream);

                string svg_string = Encoding.UTF8.GetString(stream.GetBuffer());

                using (StreamWriter writer = new StreamWriter(filename + ".svg"))
                {
                    writer.Write(svg_string);
                }

            }

        }

    }
}
