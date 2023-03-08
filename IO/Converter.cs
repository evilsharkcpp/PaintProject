using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


using Interfaces;

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
            string json_string = JsonConvert.SerializeObject(figures);

            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.Write(json_string);
            }
        }

    }
}
