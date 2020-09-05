using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.BehaviorHandlers
{
    public class NewLineBehaviorHandler : IEscapeRouteBehaviorHandler<NewLineBehavior>
    {
        public Task<string> EscapeAsync(string raw, NewLineBehavior behavior)
        {
            var escaped = HandleCarriageReturnBehavior(raw, behavior);
            return Task.FromResult(escaped);
        }

        private static string HandleCarriageReturnBehavior(string raw, NewLineBehavior behavior)
        {
            string escaped = null;

            switch (behavior)
            {
                case NewLineBehavior.Escape:
                {
                    // Replace new line characters with \n.
                    Regex regex = new Regex("\n");
                    escaped = regex.Replace(raw, @"\n");
                    break;
                }
                case NewLineBehavior.Strip:
                default:
                    // Remove new line characters.
                    escaped = raw.Replace("\n", "");
                    break;
            }

            return escaped;
        }
    }
}