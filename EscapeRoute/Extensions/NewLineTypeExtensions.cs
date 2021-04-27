using System;
using EscapeRoute.Abstractions.Enums;

namespace EscapeRoute.Extensions
{
    public static class NewLineTypeExtensions
    {
        public static string GetNewLineString(this NewLineType newLineType)
        {
            string delimiter = newLineType switch
            {
                NewLineType.Strip => "",
                NewLineType.Space => " ",
                NewLineType.Unix => @"\n",
                NewLineType.Windows => @"\r\n",
                NewLineType.Escape => @"\n",
                _ => throw new ArgumentException($"Not a valid {nameof(NewLineType)}", nameof(newLineType))
            };

            return delimiter;
        }
    }
}