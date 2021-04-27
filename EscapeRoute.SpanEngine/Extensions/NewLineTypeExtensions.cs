using System;
using EscapeRoute.Abstractions.Enums;

namespace EscapeRoute.SpanEngine.Extensions
{
    public static class NewLineTypeExtensions
    {
        public static string GetNewLineString(this NewLineType newLineType)
        {
            string delimiter = newLineType switch
            {
                NewLineType.Strip => "",
                NewLineType.Space => " ",
                NewLineType.Escape => @"\n",
                _ => throw new ArgumentException($"Not a valid {nameof(NewLineType)}", nameof(newLineType))
            };

            return delimiter;
        }
    }
}