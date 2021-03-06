using System.IO;
using System.Threading.Tasks;

namespace EscapeRoute.Abstractions.Interfaces
{
    public interface IEscapeRouter
    {
        Task<string> ParseAsync(TextReader textReader);

        Task<string> ParseAsync(string inputString);
    }
}