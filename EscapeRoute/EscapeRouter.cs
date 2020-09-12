using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
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
        /// <returns>A JSON friendly <see cref="String"/>.</returns>
        public string ParseString(string inputString)
        {
            return ReadStringAsync(inputString).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Parses a <see cref="String"/> into a JSON friendly string asynchronously.
        /// </summary>
        /// <param name="inputString">The string to parse.</param>
        /// <returns>A JSON friendly <see cref="String"/>.</returns>
        public async Task<string> ParseStringAsync(string inputString)
        {
            return await ReadStringAsync(inputString);
        }

        /// <summary>
        /// Parses a file into a JSON friendly string synchronously.
        /// </summary>
        /// <param name="fileLocation">The string to parse.</param>
        /// <returns>A JSON friendly <see cref="String"/>.</returns>
        public string ParseFile(string fileLocation)
        {
            return ReadFileAsync(fileLocation).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Parses a file into a JSON friendly string asynchronously.
        /// </summary>
        /// <param name="fileLocation">The string to parse.</param>
        /// <returns>A JSON friendly <see cref="String"/>.</returns>
        public async Task<string> ParseFileAsync(string fileLocation)
        {
            return await ReadFileAsync(fileLocation);
        }

        private void ApplyConfiguration(EscapeRouteConfiguration escapeRouteConfiguration)
        {
            _configuration = escapeRouteConfiguration;
        }

        private async Task<string> ReadStringAsync(string inputString)
        {
            using var stringReader = new StringReader(inputString);
            var escaped = await Escape(stringReader);
            
            return escaped;
        }

        private async Task<string> ReadFileAsync(string fileLocation)
        {
            using StreamReader streamReader = new StreamReader(fileLocation);
            var escaped = await Escape(streamReader);

            return escaped;
        }

        private async Task<string> Escape(TextReader textReader)
        {
            var escapeTaskList = new List<Task<string>>();
            var lines = textReader.ToLines();

            foreach (var line in lines)
            {
                escapeTaskList.Add(EscapeLine(line));
            }

            var escapedLines = await Task.WhenAll(escapeTaskList);

            var newLineString = _configuration.NewLineType.GetNewLineString();

            var escaped = string.Join(newLineString, escapedLines);

            return escaped;
        }

        /// <summary>
        /// Replace each escapable character with it's escaped string.
        /// </summary>
        /// <param name="rawString"><see cref="String"/> Raw String</param>
        /// <returns>Escaped and trimmed <see cref="String"/></returns>
        private async Task<string> EscapeLine(string rawString)
        {
            string escaped = rawString;

            // Handle backslash ('\') characters.
            escaped = await _configuration.BackslashBehaviorHandler
                                          .EscapeAsync(escaped, _configuration.BackslashBehavior);

            // Handle form feed characters.
            escaped = await _configuration.FormFeedBehaviorHandler
                                          .EscapeAsync(escaped, _configuration.FormFeedBehavior);

            // Handle tabs \t.
            escaped = await _configuration.TabBehaviorHandler
                                          .EscapeAsync(escaped, _configuration.TabBehavior);

            // Handle backspace ('\b') characters.
            escaped = await _configuration.BackspaceBehaviorHandler
                                          .EscapeAsync(escaped, _configuration.BackspaceBehavior);

            // Handle unicode \uXXXX characters.
            escaped = await _configuration.UnicodeBehaviorHandler
                                          .EscapeAsync(escaped, _configuration.UnicodeBehavior);

            // Handle trimming.
            escaped = await _configuration.TrimBehaviorHandler
                                          .EscapeAsync(escaped, _configuration.TrimBehavior);

            // Handle double quotes.
            escaped = await _configuration.DoubleQuoteBehaviorHandler
                                          .EscapeAsync(escaped, _configuration.DoubleQuoteBehavior);

            // Handle single quotes.
            escaped = await _configuration.SingleQuoteBehaviorHandler
                                          .EscapeAsync(escaped, _configuration.SingleQuoteBehavior);

            // Sequentially run each custom behavior handler (if any).
            foreach (var customHandler in _configuration.CustomBehaviorHandlers)
            {
                escaped = await customHandler.EscapeAsync(escaped);
            }

            return escaped;
        }

        
    }
}
