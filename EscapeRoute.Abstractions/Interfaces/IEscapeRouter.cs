using System;
using System.IO;
using System.Threading.Tasks;

namespace EscapeRoute.Abstractions.Interfaces
{
    public interface IEscapeRouter
    {
        [Obsolete("Method replaced with ParseAsync overload accepting a string."
                  + "Raise an issue at https://github.com/JackWFinlay/EscapeRoute/issues if this adversely affects you.")]
        string ParseString(string inputString);

        [Obsolete("Method replaced with ParseAsync overload accepting a TextReader. "
                  + "Future versions may remove support for parsing from a file. "
                  + "See: https://docs.microsoft.com/en-us/dotnet/api/system.io.textreader "
                  + "for details on handling reading of files yourself."
                  + "Raise an issue at https://github.com/JackWFinlay/EscapeRoute/issues if this adversely affects you.")]
        string ParseFile(string fileLocation);

        [Obsolete("Method replaced with ParseAsync overload accepting a string."
                  + "Raise an issue at https://github.com/JackWFinlay/EscapeRoute/issues if this adversely affects you.")]
        Task<string> ParseStringAsync(string inputString);

        [Obsolete("Method replaced with ParseAsync overload accepting a TextReader. "
                  + "Future versions may remove support for parsing from a file. "
                  + "See: https://docs.microsoft.com/en-us/dotnet/api/system.io.textreader "
                  + "for details on handling reading of files yourself."
                  + "Raise an issue at https://github.com/JackWFinlay/EscapeRoute/issues if this adversely affects you.")]
        Task<string> ParseFileAsync(string fileLocation);

        Task<string> ParseAsync(TextReader textReader);

        Task<string> ParseAsync(string inputString);
    }
}