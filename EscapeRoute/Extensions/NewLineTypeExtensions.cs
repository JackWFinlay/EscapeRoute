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
                NewLineType.None => "",
                NewLineType.Space => " ",
                NewLineType.Unix => @"\n",
                NewLineType.Windows => @"\r\n",
                _ => throw new ArgumentException($"Not a valid {nameof(NewLineType)}", nameof(newLineType))
            };

            return delimiter;
        }
    }
}