using System;
using System.Threading.Tasks;

namespace JackWFinlay.EscapeRoute
{
    public interface IEscapeRoute
    {
        String ParseString(String inputString);

        String ParseFile(String fileLocation);

        Task<String> ParseStringAsync(String inputString);

        Task<String> ParseFileAsync(String fileLocation);
    }
}