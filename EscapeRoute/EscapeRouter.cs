using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using EscapeRoute.ReplacementEngines;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute
{
    public class EscapeRouter : IEscapeRouter
    {
        private readonly SpanReplacementEngine _replacementEngine;

        /// <summary>
        /// Creates a new <see cref="EscapeRouter"/> object.
        /// </summary>
        public EscapeRouter()
        {
            var configuration = new EscapeRouteConfiguration();
            _replacementEngine = new SpanReplacementEngine(configuration);
        }

        /// <summary>
        /// Creates a new <see cref="EscapeRouter"/> object.
        /// </summary>
        public EscapeRouter(EscapeRouteConfiguration escapeRouteConfiguration)
        {
            _replacementEngine = new SpanReplacementEngine(escapeRouteConfiguration);
        }

        /// <summary>
        /// Parses content of any <see cref="TextReader"/> or equivalent into a JSON friendly string asynchronously.
        /// </summary>
        /// <param name="textReader"><see cref="TextReader"/> containing content to escape.</param>
        /// <returns>A JSON friendly <see cref="string"/>.</returns>
        public async Task<string> ParseAsync(TextReader textReader) => await EscapeAsync(textReader);

        public async Task<string> ParseAsync(string inputString) => await EscapeAsync(inputString);

        private async Task<string> EscapeAsync(string text)
        {
            var escaped = await EscapeAsync(text.AsMemory());
            
            return escaped;
        }
        
        private async Task<string> EscapeAsync(TextReader textReader)
        {
            var text = await textReader.ReadToEndAsync();

            var escaped = await EscapeAsync(text);

            return escaped;
        }

        private async Task<string> EscapeAsync(ReadOnlyMemory<char> input)
        {
            var escaped = await _replacementEngine.ReplaceAsync(input);

            return escaped;
        }
    }
}
