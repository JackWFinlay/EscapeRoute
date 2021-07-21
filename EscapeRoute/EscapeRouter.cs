using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using EscapeRoute.ReplacementEngines;
using EscapeRoute.Abstractions.Interfaces;
using EscapeRoute.Configuration;

namespace EscapeRoute
{
    public class EscapeRouter : IEscapeRouter
    {
        private readonly IReplacementEngine _replacementEngine;

        /// <summary>
        /// Creates a new <see cref="EscapeRouter"/> object.
        /// </summary>
        public EscapeRouter()
        {
            var configuration = new CharReplacementConfiguration();
            _replacementEngine = new CharReplacementEngine(configuration);
        }

        /// <summary>
        /// Creates a new <see cref="EscapeRouter"/> object with the specified configuration.
        /// </summary>
        public EscapeRouter(CharReplacementConfiguration charReplacementConfiguration)
        {
            _replacementEngine = new CharReplacementEngine(charReplacementConfiguration);
        }
        
        /// <summary>
        /// Creates a new <see cref="EscapeRouter"/> object with the specified configuration.
        /// </summary>
        public EscapeRouter(TokenReplacementConfiguration config)
        {
            _replacementEngine = new TokenReplacementEngine(config);
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
            var escapedMemory = await _replacementEngine.ReplaceAsync(input);

            var escaped = CreateString(escapedMemory);

            return escaped;
        }

        private string CreateString(ReadOnlyMemory<char> input)
        {
#if NETSTANDARD2_0
            var stringBuilder = new StringBuilder();
            var span = input.Span;
            
            for (var i = 0; i < input.Length; i++)
            {
                stringBuilder.Append(span[i]);
            }

            var escaped = stringBuilder.ToString();
#else
            var escaped = string.Create(input.Length, input,
                (destination, state) => state.Span.CopyTo(destination));
#endif
            return escaped;
        }
    }
}
