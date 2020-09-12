using System.Collections.Generic;
using System.IO;

namespace EscapeRoute.Extensions
{
    public static class TextReaderExtensions
    {
        public static IEnumerable<string> ToLines(this TextReader textReader)
        {
            string line;
            while ((line = textReader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}