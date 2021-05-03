# EscapeRoute
[![NuGet](https://img.shields.io/nuget/v/EscapeRoute.svg)](https://www.nuget.org/packages/EscapeRoute)

Selectively trim and escape text files into JSON friendly strings. Supports all JSON escape characters and Unicode characters that can be represented in the form \u(4 digit hex).

## Version `0.0.2` Notes
Please be aware of the following changes for version `0.0.2`:
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

## Version `0.0.3` Notes
Please be aware of the following changes:
- Addition of the `EscapeRoute.SpanEngine` project. 
  This project uses `Span<T>` under the hood to [speed up execution time and lower allocations and GC events](#benchmarks).
  You can use the NuGet package `EscapeRoute.SpanEngine` in place of the original `EscapeRoute` package,
  but there are changes to the `EscapeRouteConfiguration` that must be used. 
  See [EscapeRoute.SpanEngine configuration](#escaperoutespanengine-configuration) for details.

- Addition of the `Escape` value to the `NewLineType` Enum.
  This is the same as using the value `Unix` in previous versions. 
  It is there for the `EscapeRoute.SpanEngine` project which uses a different method of substitution.
   
- Addition of `CarriageReturnbehavior` and different `NewLineType` default for `EscapeRoute.SpanEngine`.
  See [EscapeRoute.SpanEngine behaviors](#escaperoutespanengine-behaviors) for details.
  
- The current characters that `EscapeRoute.SpanEngine` can escape is limited to the same that the original version `EscapeRoute`
can handle. There are plans to expand this to some of the other special characters, please raise an 
  [issue](https://github.com/JackWFinlay/EscapeRoute/issues) for any requests.

## Supported behaviors
### EscapeRoute Behaviors
The original `EscapeRoute` `EscapeRouter` currently supports the following behaviors/special characters (***Default***):

 - Tab (\t):
   - ***Strip***
   - Escape
 - New line (\n | \r\n):
   - None
   - Escape (\n)
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

### EscapeRoute.SpanEngine Behaviors
The `EscapeRoute.SpanEngine` `EscapeRouter` currently supports the following behaviors/special characters (***Default***):

- Tab (\t):
    - ***Strip***
    - Escape
- Carriage Return (\r):
    - ***Strip***
    - Escape
- New line (\n):
    - Strip
    - Space
    - ***Escape***
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
- Unicode Null (\0):
    - ***Strip***
    - Escape
    - EscapeHex

Note that for New Line type, the default is different (***Strip***) for `EscapeRoute.SpanEngine`.
Also note that the trim behaviour is not available for `EscapeRoute.SpanEngine`, this may change in future -
feel free to request this under [issues](https://github.com/JackWFinlay/EscapeRoute/issues).

## Usage
### Namespace
Use the namespace `EscapeRoute`:

```C#
using EscapeRoute;
```

### Parsing
#### EscapeRoute
EscapeRoute allows the use of Files or Strings to load in the data to be escaped and trimmed. These can be called synchronously or asynchronously. 

E.g. `ParseFile` and `ParseStringAsync`

Note: The `ParseFile`, `ParseFileAsync`, `ParseString`, `ParseStringAsync` methods have been marked obsolete from version `0.0.3`.
See section [EscapeRoute.SpanEngine](#escaperoutespanengine-and-escaperoute--003) below.

test file: [test1.txt](EscapeRoute.Test/test-files/test1.txt)
```C#
using EscapeRoute;

namespace Example
{
    public class ExampleProgram
    {
        public void TestDefaultBehaviorFromFile()
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

#### EscapeRoute.SpanEngine and EscapeRoute >= `0.0.3`)
`EscapeRoute.SpanEngine` uses multiple overloads of the `ParseAsync` method to perform parsing of
either a `TextReader` or a `string`. You must provide the `TextReader` (`StreamReader`, `StringReader` etc.)
and load the file yourself.

`EscapeRoute.SpanEngine` doesn't accept file paths - 
this is also marked obsolete in version `0.0.3` of the vanilla `EscapeRoute`.

test file: [unicode1.txt](EscapeRoute.Test/test-files/unicode1.txt)
```c#
using EscapeRoute.SpanEngine;

namespace Example
{
    public async void TestUnicodeFromFileAsyncDefaultParse()
    {
        string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
        IEscapeRouter escapeRouter = new EscapeRouter();
        const string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
        using StreamReader reader = File.OpenText(fileLocation);
        string result = await escapeRouter.ParseAsync(reader);
        Assert.Equal(expected, result); // True
    }
    
    public async void TestUnicodeFromStringAsyncDefaultParse()
    {
        IEscapeRouter escapeRouter = new EscapeRouter();
        const string input = "( ͡° ͜ʖ ͡°)";
        const string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
        string result = await escapeRouter.ParseAsync(input);
        Assert.Equal(expected, result); // True
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
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
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
            string result = await escapeRouter.ParseAsync(unicodeString1);
            
            Console.WriteLine(result); 
            // "( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)"
            
            string areEqual = expected.Equals(result) ? "" : " not";
            Console.WriteLine($"The strings are{areEqual} equal"); 
            // "The strings are equal"
        }
    }
}
```

A limitation of this is that characters beyond U+FFFF cannot be escaped properly.

### Behavior Handlers

#### EscapeRoute
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

#### EscapeRoute.SpanEngine Configuration
The configuration for the `EscapeRoute.SpanEngine` `EscapeRouter` is a bit different to that of the basic `EscapeRoute` `EscapeRouter`.
You can override the default behavior handling of any of the default behaviors
by supplying a class using the [`IEscapeRouteEscapeHandler`](EscapeRoute.SpanEngine.Abstractions/Interfaces/IEscapeRouteEscapeHandler.cs) interface.
See the [EscapeHandlers](EscapeRoute.SpanEngine/EscapeHandlers) for examples.

## Benchmarks
```
BenchmarkDotNet=v0.12.1, OS=macOS 11.2.1 (20D74) [Darwin 20.3.0]
Intel Core i5-6267U CPU 2.90GHz (Skylake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.101
[Host]     : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
DefaultJob : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  
Mean      : Arithmetic mean of all measurements
Error     : Half of 99.9% confidence interval
StdDev    : Standard deviation of all measurements
Ratio     : Mean of the ratio distribution ([Current]/[Baseline])
Gen 0     : GC Generation 0 collects per 1000 operations
Gen 1     : GC Generation 1 collects per 1000 operations
Gen 2     : GC Generation 2 collects per 1000 operations
Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
1 μs      : 1 Microsecond (0.000001 sec)
```
|                        Method |     Mean |   Error |  StdDev | Ratio |    Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------ |---------:|--------:|--------:|------:|---------:|------:|------:|----------:|
|               AsciiParseAsync | 419.3 μs | 3.43 μs | 2.87 μs |  1.00 | 195.8008 |     - |     - | 399.93 KB |
|     AsciiParseAsyncSpanString | 129.0 μs | 0.48 μs | 0.40 μs |  0.31 |  20.5078 |     - |     - |  42.42 KB |
| AsciiParseAsyncSpanTextReader | 133.6 μs | 1.46 μs | 1.22 μs |  0.32 |  20.7520 |     - |     - |  42.59 KB |

|                               Method |     Mean |    Error |   StdDev | Ratio |    Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------- |---------:|---------:|---------:|------:|---------:|------:|------:|----------:|
|               AsciiUnicodeParseAsync | 781.2 μs | 15.26 μs | 20.37 μs |  1.00 | 333.0078 |     - |     - | 681.77 KB |
|     AsciiUnicodeParseAsyncSpanString | 230.8 μs |  4.44 μs |  4.16 μs |  0.30 |  55.4199 |     - |     - | 113.53 KB |
| AsciiUnicodeParseAsyncSpanTextReader | 233.5 μs |  1.45 μs |  1.21 μs |  0.30 |  55.4199 |     - |     - |  113.7 KB |


As shown by the results above, the `EscapeRoute.SpanEngine` is more than three times as fast,
allocates considerably less memory, and performs far less GC events for the same given
[test data](EscapeRoute.Benchmarks/ReplacementEngine/Constants.cs).

## License
MIT

See [LICENSE](LICENSE) for details.
