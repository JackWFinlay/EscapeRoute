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
    public class EscapeRouter : IEscapeRoute
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

            var newLineString = GetNewLineString(_configuration.NewLineType);

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

            // Remove tabs \t.
            if (_configuration.TabBehavior == TabBehavior.Strip)
            {
                escaped = escaped.Replace("\t", "");
            }
            // Replace tabs with \t.
            else if (_configuration.TabBehavior == TabBehavior.Escape)
            {
                Regex regex = new Regex("\t");
                escaped = regex.Replace(escaped, @"\t");
            }

            // Handle backspace ('\b') characters.
            escaped = await _configuration.BackspaceBehaviorHandler
                                          .EscapeAsync(escaped, _configuration.BackspaceBehavior);

            // Remove unicode \uXXXX characters.
            if (_configuration.UnicodeBehavior == UnicodeBehavior.Strip)
            {
                // [^\x00-\x7F] matches any non-ASCII character.
                Regex regex = new Regex(@"[^\x00-\x7F]");
                escaped = regex.Replace(escaped, "");
            }
            // Replace unicode characters with \uXXXX.
            else if (_configuration.UnicodeBehavior == UnicodeBehavior.Escape)
            {
                // https://stackoverflow.com/a/25349901
                escaped = Regex.Replace(escaped, @"[^\x00-\x7F]", c => string.Format(@"\u{0:x4}", (int)c.Value[0]));
            }

            // Trim spaces at beginning of string.
            if (_configuration.TrimBehavior == TrimBehavior.Start ||
                _configuration.TrimBehavior == TrimBehavior.Both )
            {
                escaped = escaped.TrimStart();
            }
            // Trim spaces at end of string.
            else if (_configuration.TrimBehavior == TrimBehavior.End ||
                    _configuration.TrimBehavior == TrimBehavior.Both )
            {
                escaped = escaped.TrimEnd();
            }

            escaped = escaped.Replace("\"", @"\""");
            escaped = escaped.Replace("\'", @"\'");

            return escaped;
        }

        private static string GetNewLineString(NewLineType newLineType)
        {
            var delimiter = newLineType switch
            {
                NewLineType.None => "",
                NewLineType.Space => " ",
                NewLineType.Unix => @"\n",
                NewLineType.Windows => @"\r\n",
                _ => throw new ArgumentException($"Not a valid {nameof(NewLineType)}", nameof(newLineType))
            };

            return delimiter;
        }
    }
}
