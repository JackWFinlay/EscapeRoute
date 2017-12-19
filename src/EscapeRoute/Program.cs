using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace JackWFinlay.EscapeRoute
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
    }
}
