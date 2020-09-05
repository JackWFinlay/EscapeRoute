using System.Threading.Tasks;

namespace EscapeRoute.Abstractions.Interfaces
{
    public interface IEscapeRouter
    {
        string ParseString(string inputString);

        string ParseFile(string fileLocation);

        Task<string> ParseStringAsync(string inputString);

        Task<string> ParseFileAsync(string fileLocation);
    }
}