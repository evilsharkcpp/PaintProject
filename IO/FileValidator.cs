using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    public static class FileValidator
    {
        public static HashSet<string> SVG_EXTENSION = new HashSet<string> { "svg" };
        public static HashSet<string> PNG_EXTENSION = new HashSet<string> { "png" };
        public static HashSet<string> JPEG_EXTENSION = new HashSet<string> { "jpg", "jpeg" };
        public static HashSet<string> JSON_EXTENSION = new HashSet<string> { "json" };

        public static void CheckParentDirectory(string filename)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filename)))
            {
                throw new DirectoryNotFoundException($"parent directory of file {filename} does not exist");
            }
        }

        public static void CheckFileExists(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"file {filename} does not exist");
            }
        }
    }
}
