using System;
using System.IO;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.SpanEngine
{
    public class EscapeRouter : IEscapeRouter
    {
        private EscapeRouteConfiguration _configuration;

        /// <summary>
        /// Creates a new <see cref="EscapeRouter"/> object.
        /// </summary>
        public EscapeRouter()
        {
            _configuration = new EscapeRouteConfiguration();
        }

        /// <summary>
        /// Creates a new <see cref="EscapeRouter"/> object.
        /// </summary>
        public EscapeRouter(EscapeRouteConfiguration escapeRouteConfiguration)
        {
            ApplyConfiguration(escapeRouteConfiguration);
        }

        /// <summary>
        /// Parses a <see cref="String"/> into a JSON friendly string synchronously.
        /// </summary>
        /// <param name="inputString">The string to parse.</param>
        /// <returns>A JSON friendly <see cref="string"/>.</returns>
        public string ParseString(string inputString)
        {
            return ReadStringAsync(inputString).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Parses a <see cref="String"/> into a JSON friendly string asynchronously.
        /// </summary>
        /// <param name="inputString">The string to parse.</param>
        /// <returns>A JSON friendly <see cref="string"/>.</returns>
        public async Task<string> ParseStringAsync(string inputString)
        {
            return await ReadStringAsync(inputString);
        }

        /// <summary>
        /// Parses a file into a JSON friendly string synchronously.
        /// </summary>
        /// <param name="fileLocation">The string to parse.</param>
        /// <returns>A JSON friendly <see cref="string"/>.</returns>
        public string ParseFile(string fileLocation)
        {
            return ReadFileAsync(fileLocation).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Parses a file into a JSON friendly string asynchronously.
        /// </summary>
        /// <param name="fileLocation">The string to parse.</param>
        /// <returns>A JSON friendly <see cref="string"/>.</returns>
        public async Task<string> ParseFileAsync(string fileLocation)
        {
            return await ReadFileAsync(fileLocation);
        }

        /// <summary>
        /// Parses content of any <see cref="TextReader"/> or equivalent into a JSON friendly string asynchronously.
        /// </summary>
        /// <param name="textReader"><see cref="TextReader"/> containing content to escape.</param>
        /// <returns>A JSON friendly <see cref="string"/>.</returns>
        public async Task<string> ParseAsync(TextReader textReader) => await EscapeAsync(textReader);

        public async Task<string> ParseAsync(string inputString) => await EscapeAsync(inputString);

        private void ApplyConfiguration(EscapeRouteConfiguration escapeRouteConfiguration)
        {
            _configuration = escapeRouteConfiguration;
        }

        private async Task<string> ReadStringAsync(string inputString)
        {
            var escaped = await EscapeAsync(inputString);
            
            return escaped;
        }

        private async Task<string> ReadFileAsync(string fileLocation)
        {
            using StreamReader streamReader = new StreamReader(fileLocation);
            var escaped = await EscapeAsync(streamReader);

            return escaped;
        }

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
            var escapedMemory = await _configuration.ReplacementEngine.ReplaceAsync(input, _configuration);

            var escaped = string.Create(escapedMemory.Length, escapedMemory,
                (destination, state) => state.Span.CopyTo(destination));

            return escaped;
        }
    }
}
