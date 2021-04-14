using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Interfaces;
using EscapeRoute.Extensions;

namespace EscapeRoute
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

        public async Task<string> ParseAsync(string inputString) => await ReadStringAsync(inputString);

        private void ApplyConfiguration(EscapeRouteConfiguration escapeRouteConfiguration)
        {
            _configuration = escapeRouteConfiguration;
        }

        private async Task<string> ReadStringAsync(string inputString)
        {
            using var stringReader = new StringReader(inputString);
            var escaped = await EscapeAsync(stringReader);
            
            return escaped;
        }

        private async Task<string> ReadFileAsync(string fileLocation)
        {
            using StreamReader streamReader = new StreamReader(fileLocation);
            var escaped = await EscapeAsync(streamReader);

            return escaped;
        }

        private async Task<string> EscapeAsync(TextReader textReader)
        {
            var escapeTaskList = new List<Task<ReadOnlyMemory<char>>>();
            var lines = textReader.ToLines();

            foreach (var line in lines)
            {
                escapeTaskList.Add(EscapeLine(line));
            }

            var escapedLines = await Task.WhenAll(escapeTaskList);

            var newLineString = _configuration.NewLineType.GetNewLineString().AsMemory();

            var joinedMemory = escapedLines.Join(newLineString);

            var escaped = string.Create(joinedMemory.Length, joinedMemory,
                (destination, state) => state.Span.CopyTo(destination));

            return escaped;
        }

        /// <summary>
        /// Replace each escapable character with it's escaped string.
        /// </summary>
        /// <param name="rawString"><see cref="String"/> Raw String</param>
        /// <returns>Escaped and trimmed <see cref="String"/></returns>
        private async Task<ReadOnlyMemory<char>> EscapeLine(string rawString)
        {
            var escaped = rawString.AsMemory();

            // Handle trimming.
            escaped = await _configuration.TrimBehaviorHandler
                .EscapeAsync(escaped, _configuration.TrimBehavior, _configuration.ReplacementEngine);

            escaped = await _configuration.ReplacementEngine.ReplaceAsync(escaped, _configuration);
            
            // Handle unicode \uXXXX characters.
            escaped = await _configuration.UnicodeBehaviorHandler
                .EscapeAsync(escaped, _configuration.UnicodeBehavior, _configuration.ReplacementEngine);


            // Sequentially run each custom behavior handler (if any).
            foreach (var customHandler in _configuration.CustomBehaviorHandlers)
            {
                escaped = await customHandler.EscapeAsync(escaped);
            }

            return escaped;
        }

        
    }
}
