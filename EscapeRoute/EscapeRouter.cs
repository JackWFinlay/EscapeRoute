using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

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
            return ReadString(inputString).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Parses a <see cref="String"/> into a JSON friendly string asynchronously.
        /// </summary>
        /// <param name="inputString">The string to parse.</param>
        /// <returns>A JSON friendly <see cref="String"/>.</returns>
        public async Task<string> ParseStringAsync(string inputString)
        {
            return await ReadString(inputString);
        }

        /// <summary>
        /// Parses a file into a JSON friendly string synchronously.
        /// </summary>
        /// <param name="fileLocation">The string to parse.</param>
        /// <returns>A JSON friendly <see cref="String"/>.</returns>
        public string ParseFile(string fileLocation)
        {
            return ReadFile(fileLocation).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Parses a file into a JSON friendly string asynchronously.
        /// </summary>
        /// <param name="fileLocation">The string to parse.</param>
        /// <returns>A JSON friendly <see cref="String"/>.</returns>
        public async Task<string> ParseFileAsync(string fileLocation)
        {
            return await ReadFile(fileLocation);
        }

        private void ApplyConfiguration(EscapeRouteConfiguration escapeRouteConfiguration)
        {
            _configuration = escapeRouteConfiguration;
        }

        private async Task<string> ReadString(string inputString)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string[] parts = inputString.Split('\n');

            // Sets newLine variable to whatever is expected. \n if behaviour is escape.
            string newLine = "";
            if (_configuration.NewLineBehavior == NewLineBehavior.Escape)
            {
                newLine = @"\n";
            }

            // Append first line without \n literal
            stringBuilder.Append(await Escape(parts[0]));

            // For the rest of the lines in the string. Ignores first index, [0].
            for (var i = 1; i < parts.Length; i++)
            {
                // Escape the contents of the line and add it to the string being built.
                stringBuilder.Append(newLine + await Escape(parts[i]));
            }

            return stringBuilder.ToString();
        }

        private async Task<string> ReadFile(string fileLocation)
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (StreamReader streamReader = new StreamReader(fileLocation))
            {
                // Sets newLine variable to whatever is expected. \n if behaviour is escape.
                string newLine = "";
                if (_configuration.NewLineBehavior == NewLineBehavior.Escape)
                {
                    newLine = @"\n";
                }

                // Read first line and append without \n literal.
                string line = streamReader.ReadLine();
                stringBuilder.Append(await Escape(line));

                // Read rest of the lines, prepending newLine value.
                while ((line = streamReader.ReadLine()) != null)
                {
                    // Escape the contents of the line and add it to the string being built.
                    stringBuilder.Append(newLine + await Escape(line));
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Replace each escapable character with it's escaped string.
        /// </summary>
        /// <param name="rawString"><see cref="String"/> Raw String</param>
        /// <returns>Escaped and trimmed <see cref="String"/></returns>
        private async Task<string> Escape(string rawString)
        {
            string escaped = rawString;

            // Handle backslash ('\') characters.
            escaped = await _configuration.BackslashBehaviorHandler
                                          .EscapeAsync(escaped, _configuration.BackslashBehavior);

            // Remove form feed characters.
            if (_configuration.FormFeedBehavior == FormFeedBehavior.Strip)
            {
                escaped = escaped.Replace("\f", "");
            }
            // Replace form feed with \f.
            else if (_configuration.FormFeedBehavior == FormFeedBehavior.Escape)
            {
                Regex regex = new Regex("\f");
                escaped = regex.Replace(escaped, @"\f");
            }

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

            // Remove new line characters. To escape, the \n literal is prepended in calling methods.
            if (_configuration.NewLineBehavior == NewLineBehavior.Strip)
            {
                escaped = escaped.Replace("\n", "");
            }

            escaped = await _configuration.CarriageReturnBehaviorHandler
                                          .EscapeAsync(escaped, _configuration.CarriageReturnBehavior);

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

        
    }
}
