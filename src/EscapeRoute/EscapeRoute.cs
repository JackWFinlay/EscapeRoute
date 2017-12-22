using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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

            foreach (String part in parts)
            {
                // Escape the contents of the line and add it to the string being built.
                stringBuilder.Append(Escape(part));
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
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        // Escape the contents of the line and add it to the string being built.
                        stringBuilder.Append(Escape(line));
                    }
                }
                return stringBuilder.ToString();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // Replace each escapable character with it's escaped string.
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
                escaped = escaped.Replace("\t", @"\t");
            }

            // Remove new line characters.
            if (_escapeRouteConfiguration.NewLineBehaviour == NewLineBehavior.Strip)
            {
                escaped = escaped.Replace("\n", "");
            }
            // Replace new line characters with \n.
            else if (_escapeRouteConfiguration.NewLineBehaviour == NewLineBehavior.Escape)
            {
                escaped = escaped.Replace("\n", @"\n");
            }

            // Remove carriage return characters.
            if (_escapeRouteConfiguration.CarriageReturnBehaviour == CarriageReturnBehaviour.Strip)
            {
                escaped = escaped.Replace("\r", "");
            }
            // Replace carriage return characters with \r.
            else if (_escapeRouteConfiguration.CarriageReturnBehaviour == CarriageReturnBehaviour.Escape)
            {
                escaped = escaped.Replace("\r", @"\r");
            }

            // Remove backspace return characters.
            if (_escapeRouteConfiguration.BackspaceBehaviour == BackspaceBehaviour.Strip)
            {
                escaped = escaped.Replace("\b", "");
            }
            // Replace backspace characters with \b.
            else if (_escapeRouteConfiguration.BackspaceBehaviour == BackspaceBehaviour.Escape)
            {
                escaped = escaped.Replace("\b", @"\b");
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
