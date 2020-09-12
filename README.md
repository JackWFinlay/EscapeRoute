# EscapeRoute
[![NuGet](https://img.shields.io/nuget/v/EscapeRoute.svg)](https://www.nuget.org/packages/EscapeRoute)

Selectively trim and escape text files into JSON friendly strings. Supports all JSON escape characters and Unicode characters that can be represented in the form \u(4 digit hex).

## Version 0.0.2 Note
Please be aware of the following changes for version 0.0.2:
 - Normalization of the type name `*Behavior`, note the American English spelling.
 - Interfaces and Enums are abstracted to the `EscapeRoute.Abstractions` project. 
 These will exist in a separate NuGet package of the same name.
 - NewLine character handling has changed. Please see [Supported Behaviors](#supported-behaviors). 
 This is to cater for the use of a `TextReader`, instead of splitting the file on just `\n` characters, 
 which I hope you agree is a much better solution!
 - A change from the interface and class names `IEscapeRoute` and `EscapeRoute` to `IEscapeRouter` and `EscapeRouter` respectively.
 This is to avoid a clash with the namespace `EscapeRoute`.
 - An addition of individual `*BehaviorHandler` classes are available to customise handling of individual behaviors,
 if you wish to override the default behavior.


## Supported behaviors
Currently supports the following behaviors/special characters (***Default***):

 - Tab (\t):
   - ***Strip***
   - Escape
 - New line (\n | \r\n):
   - None
   - ***Space***
   - Unix (\n)
   - Windows (\r\n)
 - Backspace (\b):
   - ***Strip***
   - Escape
 - Form feed (\f):
   - ***Strip***
   - Escape
 - Backslash (\\\\):
   - Strip
   - ***Escape***
 - Unicode (\u1234):
   - Strip
   - ***Escape***
 - Single quotes '':
   - ***Single***
   - Double
 - Double quotes "":
   - ***Double***
   - Single
 - Trim
   - None
   - Start
   - End
   - ***Both***

## Usage
### Namespace
Use the namespace `EscapeRoute`:

```C#
using EscapeRoute;
```

### Parsing
EscapeRoute allows the use of Files or Strings to load in the data to be escaped and trimmed. These can be called synchronously or asynchronously. 

E.g. `ParseFile` and `ParseStringAsync`

test file: [test1.txt](EscapeRoute.Test/test-files/test1.txt)
```C#
using EscapeRoute;

namespace Example
{
    public class ExampleProgram
    {
        public void TestDefaultBehaviourFromFile()
        {
            string fileLocation = $"{workspaceFolder}/test-files/test1.txt";
            IEscapeRouter escapeRouter = new EscapeRouter();
            const string expected = "The quick brown fox jumps over the lazy dog.";
            string result = escapeRouter.ParseFile(fileLocation);
            
            string areEqual = expected.Equals(result) ? "" : " not";
            Console.WriteLine($"The strings are{areEqual} equal"); 
            // "The strings are equal"
        }
    }
}
```

### Configuration
EscapeRoute allows configuration using an [`EscapeRouteConfiguration`](EscapeRoute/EscapeRouteConfiguration.cs) object, which is passed to the EscapeRoute constructor:
```C#
using EscapeRoute;

namespace Example
{
    public class ExampleProgram
    {
        private static const String inputString1 = 
            "The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog.";

        public async void TestEscapeAllBehaviourFromStringAsync()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehaviour = TabBehaviour.Escape,
                BackspaceBehaviour = BackspaceBehaviour.Escape,
                TrimBehaviour = TrimBehaviour.None,
                NewLineType = NewLineType.Windows
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog.";
            string result = await escapeRouter.ParseStringAsync(inputString1);

            Console.WriteLine(result); 
            // "The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog."
            
            string areEqual = expected.Equals(result) ? "" : " not";
            Console.WriteLine($"The strings are{areEqual} equal"); 
            // "The strings are equal"
        }
    }
}
```

### Unicode
EscapeRoute supports the translation of non-ASCII Unicode characters to the JSON escape form `\u(4 hex digits)` 

E.g. `ʖ` = `\u0296`

```C#
using EscapeRoute;

namespace Example
{
    public class ExampleUnicodeProgram
    {
        private static const String unicodeString1 = "( ͡° ͜ʖ ͡°)";

        public async void TestEscapeUnicodeFromStringAsync()
        {
            // Escape is default behaviour for Unicode characters,
            // no configuration required.
            IEscapeRouter escapeRouter = new EscapeRouter();
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRouter.ParseStringAsync(unicodeString1);
            
            Console.WriteLine(result); 
            // "( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)"
            
            string areEqual = expected.Equals(result) ? "" : " not";
            Console.WriteLine($"The strings are{areEqual} equal"); 
            // "The strings are equal"
        }
    }
}
```

### Behavior Handlers
You can override the default behavior handling of any of the default behaviors 
by supplying a class using the [`IEscapeRouteBehaviorHandler`](EscapeRoute.Abstractions/Interfaces/IEscapeRouteBehaviorHandler.cs) interface.
See the [BehaviorHandlers](EscapeRoute/BehaviorHandlers) for examples.

You can also add custom behavior handlers that are evaluated sequentially after the built-in handlers,
just create a class using the [`IEscapeRouteCustomBehaviorHandler`](EscapeRoute.Abstractions/Interfaces/IEscapeRouteCustomBehaviorHandler.cs) interface, 
and pass them in a list through the [`EscapeRouteConfiguration`](EscapeRoute/EscapeRouteConfiguration.cs). e.g:
```c#
public class ExampleCustomBehaviorHandler : IEscapeRouteCustomBehaviorHandler
{
    public Task<string> EscapeAsync(string raw)
    {
        // Replace occurrences of string "dog" with string "cat", in string raw.
        string escaped = Regex.Replace(raw, "dog", "cat");

        return Task.FromResult(escaped);
    }
}
```
```c#
EscapeRouteConfiguration config = new EscapeRouteConfiguration
{
    CustomBehaviorHandlers = new List<IEscapeRouteCustomBehaviorHandler>()
    {
        new ExampleCustomBehaviorHandler()
    }
};
```

## License
MIT

See [LICENSE](LICENSE) for details.
