using System.Threading.Tasks;

namespace JackWFinlay.EscapeRoute
{
    public interface IEscapeRoute
    {
        string ParseString(string inputString);

        string ParseFile(string fileLocation);

        Task<string> ParseStringAsync(string inputString);

        Task<string> ParseFileAsync(string fileLocation);
    }
}