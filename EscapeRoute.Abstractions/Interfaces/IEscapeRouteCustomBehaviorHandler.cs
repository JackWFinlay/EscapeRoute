using System.Threading.Tasks;

namespace EscapeRoute.Abstractions.Interfaces
{
    public interface IEscapeRouteCustomBehaviorHandler
    {
        Task<string> EscapeAsync(string raw);
    }
}