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
            // var configuration = new CharReplacementConfiguration();
            // _replacementEngine = new CharReplacementEngine(configuration);
        }
        
        /// <summary>
        /// Creates a new <see cref="EscapeRouter"/> object with the specified configuration.
        /// </summary>
        public EscapeRouter(CharReplacementConfiguration charReplacementConfiguration)
        {
            // _replacementEngine = new CharReplacementEngine(charReplacementConfiguration);
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

        public async Task<string> ParseAsync(string input) => await EscapeAsync(input);

        public async Task<string> ParseAsync(ReadOnlyMemory<char> input) => await EscapeAsync(input);

        private async Task<string> EscapeAsync(string text)
        {
            var escaped = await ParseAsync(text.AsMemory());
            
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
            var escapedMemory = await _replacementEngine.ReplaceAsync(input, out var length);

            if (escapedMemory.IsEmpty)
            {
                escapedMemory = new ReadOnlyMemory<ReadOnlyMemory<char>>(new[] { input });
                length = input.Length;
            }

            var escaped = CreateString(escapedMemory, length);

            return escaped;
        }

        private string CreateString(ReadOnlyMemory<ReadOnlyMemory<char>> input, int length)
        {
#if NETSTANDARD2_0
            var stringBuilder = new StringBuilder();
            var span = input.Span;
            
            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < span[i].Length; i++)
                {
                    stringBuilder.Append(span[i].Span[j]);
                }
            }
            var escaped = stringBuilder.ToString();
#else
                var escaped = string.Create(length, input,
                (destination, state) =>
                {
                    int prevLength = 0;
                    for (var i = 0; i < input.Length; i++)
                    {
                        state.Span[i].Span.CopyTo(destination.Slice(prevLength));
                        prevLength += state.Span[i].Length;
                    }
                });
#endif
            return escaped;
        }
    }
}
