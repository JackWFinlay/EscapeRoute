using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.CodeDom;

namespace JackWFinlay.EscapeRoute
{
    public class EscapeRoute : IEscapeRoute
    {
        private EscapeRouteConfiguration _escapeRouteConfiguration;

        /// <summary>
        /// Creates a new <see cref="EscapeRoute"/> object.
        /// </summary>
        public EscapeRoute()
        {
            _escapeRouteConfiguration = new EscapeRouteConfiguration();
        }

        /// <summary>
        /// Creates a new <see cref="EscapeRoute"/> object.
        /// </summary>
        public EscapeRoute(EscapeRouteConfiguration escapeRouteConfiguration)
        {
            ApplyConfiguration(escapeRouteConfiguration);
        }

        /// <summary>
        /// Parses a <see cref="String"/> into a JSON friendly string synchronously.
        /// <param name="inputString">The string to parse.</param>
        /// <returns>A JSON friendly <see cref="String"/>.</returns>
        public String ParseString(string inputString)
        {
            return ReadString(inputString);
        }

        /// <summary>
        /// Parses a <see cref="String"/> into a JSON friendly string asynchronously.
        /// <param name="inputString">The string to parse.</param>
        /// <returns>A JSON friendly <see cref="String"/>.</returns>
        public async Task<String> ParseStringAsync(String inputString)
        {
            return await Task.Run(() => ReadString(inputString));
        }

        /// <summary>
        /// Parses a file into a JSON friendly string synchronously.
        /// <param name="fileLocation">The string to parse.</param>
        /// <returns>A JSON friendly <see cref="String"/>.</returns>
        public String ParseFile(string fileLocation)
        {
            return ReadFile(fileLocation);
        }

        /// <summary>
        /// Parses a file into a JSON friendly string asynchronously.
        /// <param name="fileLocation">The string to parse.</param>
        /// <returns>A JSON friendly <see cref="String"/>.</returns>
        public async Task<String> ParseFileAsync(String fileLocation)
        {
            return await Task.Run(() => ReadFile(fileLocation));
        }

        private void ApplyConfiguration(EscapeRouteConfiguration escapeRouteConfiguration)
        {
            this._escapeRouteConfiguration = escapeRouteConfiguration;
        }

        private String ReadString(String inputString)
        {
            StringBuilder stringBuilder = new StringBuilder();
            String[] parts = inputString.Split('\n');

            String newLine = "";
            if (_escapeRouteConfiguration.NewLineBehaviour == NewLineBehaviour.Escape)
            {
                newLine = @"\n";
            }

            // Append first line without \n literal
            stringBuilder.Append(Escape(parts[0]));

            // For the rest of the lines in the string.
            for (var i = 1; i < parts.Length; i++)
            {
                // Escape the contents of the line and add it to the string being built.
                stringBuilder.Append(newLine + Escape(parts[i]));
            }

            return stringBuilder.ToString();
        }

        private String ReadFile(String fileLocation)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string line;

            try {
                using (StreamReader streamReader = new StreamReader(fileLocation))
                {
                    String newLine = "";
                    if (_escapeRouteConfiguration.NewLineBehaviour == NewLineBehaviour.Escape)
                    {
                        newLine = @"\n";
                    }

                    // Read first line and apppend without \n literal.
                    line = streamReader.ReadLine();
                    stringBuilder.Append(Escape(line));

                    // Read rest of the lines, prepending newLine value.
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        // Escape the contents of the line and add it to the string being built.
                        stringBuilder.Append(newLine + Escape(line));
                    }
                }
                return stringBuilder.ToString();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Replace each escapable character with it's escaped string.
        /// </summary>
        /// <param><see cref="String"/> rawString</param>
        /// <returns>Escaped and trimmed <see cref="String"/><returns>
        private String Escape(string rawString)
        {
            string escaped = rawString;

            // Remove tabs if '-T' flag is present in args.
            if (_escapeRouteConfiguration.TabBehaviour == TabBehaviour.Strip)
            {
                escaped = escaped.Replace("\t", "");
            }
            // Replace tabs with \t.
            else if (_escapeRouteConfiguration.TabBehaviour == TabBehaviour.Escape)
            {
                Regex regex = new Regex("\t");
                escaped = regex.Replace(escaped, @"\t");
            }

            // Remove new line characters. To escape, the \n literal is prepended in calling methods.
            if (_escapeRouteConfiguration.NewLineBehaviour == NewLineBehaviour.Strip)
            {
                escaped = escaped.Replace("\n", "");
            }

            // Remove carriage return characters.
            if (_escapeRouteConfiguration.CarriageReturnBehaviour == CarriageReturnBehaviour.Strip)
            {
                escaped = escaped.Replace("\r", "");
            }
            // Replace carriage return characters with \r.
            else if (_escapeRouteConfiguration.CarriageReturnBehaviour == CarriageReturnBehaviour.Escape)
            {
                Regex regex = new Regex("\r");
                escaped = regex.Replace(escaped, @"\r");
            }

            // Remove backspace return characters.
            if (_escapeRouteConfiguration.BackspaceBehaviour == BackspaceBehaviour.Strip)
            {
                escaped = escaped.Replace("\b", "");
            }
            // Replace backspace characters with \b.
            else if (_escapeRouteConfiguration.BackspaceBehaviour == BackspaceBehaviour.Escape)
            {
                Regex regex = new Regex("\b");
                escaped = regex.Replace(escaped, @"\b");
            }

            // Trim spaces at begining of string.
            if (_escapeRouteConfiguration.TrimBehaviour == TrimBehaviour.Start ||
                _escapeRouteConfiguration.TrimBehaviour == TrimBehaviour.Both )
            {
                escaped = escaped.TrimStart();;
            }
            // Trim spaces at end of string.
            else if (_escapeRouteConfiguration.TrimBehaviour == TrimBehaviour.End ||
                    _escapeRouteConfiguration.TrimBehaviour == TrimBehaviour.Both )
            {
                escaped = escaped.TrimEnd();
            }

            escaped = escaped.Replace("\"", @"\""");
            escaped = escaped.Replace("\'", @"\'");

            return escaped;
        }
    }
}
