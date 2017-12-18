using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoute
{
    class Program
    {
        private static List<string> _argsList;
        private static CommandArgument _fileLocationArgument;
        private static CommandOption _tabOption,
                                    _newlineOption,
                                    _carriageReturnOption,
                                    _backspaceOption,
                                    _trimOption;
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.Name = "EscapeRoute";
            app.HelpOption("-?|-h|--help");

            app.Command("escape", (command) =>
            {
                _fileLocationArgument = command.Argument("[file]",
                                        "file to escape",
                                        true);

                _tabOption = app.Option("-t|--tabs <behaviour>",
                                        "Escape or strip tab characters; Options: escape, strip; Default: strip",
                                        CommandOptionType.SingleValue);

                _newlineOption = app.Option("-n|--newline <behaviour>",
                                        "Escape or strip new line characters; Options: escape, strip; Default: strip",
                                        CommandOptionType.SingleValue);

                _carriageReturnOption = app.Option("-r|--return <behaviour>",
                                        "Escape or strip carriage return characters; Options: escape, strip; Default: strip",
                                        CommandOptionType.SingleValue);

                _backspaceOption = app.Option("-b|--backspace <behaviour>",
                                        "Escape or strip backspace characters; Options: escape, strip; Default: strip",
                                        CommandOptionType.SingleValue);

                _trimOption = app.Option("-T|--trim <behaviour>",
                                        "Trim start or end of lines; Options: start, end; Default: not specified",
                                        CommandOptionType.MultipleValue);

                command.OnExecute(async () =>
                {
                    await ReadFile();
                });
            });
        }

        // private static List<string> ParseArgs(string[] args)
        // {
        //     List<string> argList = new List<string>();

        //     // Loop through all args and add them to List
        //     foreach (string arg in args)
        //     {
        //         argList.Add(arg);
        //     }

        //     return argList;
        // }

        private async static Task<string> ReadFile()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string line;

            using (StreamReader streamReader = new StreamReader(_fileLocationArgument.Value))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    // Escape the contents of the line and add it to the string being built.
                    stringBuilder.Append(Escape(line));
                }
            }
            return "";
        }

        // Replace each escapable character with it's escaped string.
        private static string Escape(string rawString)
        {
            string escaped = rawString;

            // Remove tabs if '-T' flag is present in args.
            if (_argsList.Contains("-T"))
            {
                escaped = escaped.Replace("\t", "");
            }
            // Replace tabs with \t.
            else if (_argsList.Contains("-t"))
            {
                escaped = escaped.Replace("\t", @"\t");
            }

            // Remove new line characters.
            if (_argsList.Contains("-N"))
            {
                escaped = escaped.Replace("\n", "");
            }
            // Replace new line characters with \n.
            else if (_argsList.Contains("-n"))
            {
                escaped = escaped.Replace("\n", @"\n");
            }

            // Remove carriage return characters.
            if (_argsList.Contains("-R"))
            {
                escaped = escaped.Replace("\r", "");
            }
            // Replace carriage return characters with \r.
            else if (_argsList.Contains("-r"))
            {
                escaped = escaped.Replace("\r", @"\r");
            }

            // Remove backspace return characters.
            if (_argsList.Contains("-B"))
            {
                escaped = escaped.Replace("\b", "");
            }
            // Replace backspace characters with \b.
            else if (_argsList.Contains("-b"))
            {
                escaped = escaped.Replace("\b", @"\b");
            }

            // Trim spaces at begining of string.
            if (_argsList.Contains("-S"))
            {
                escaped = escaped.TrimStart();;
            }
            // Trim spaces at end of string.
            else if (_argsList.Contains("-s"))
            {
                escaped = escaped.TrimEnd();
            }

            return escaped;
        }
    }
}
