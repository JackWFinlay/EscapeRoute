# EscapeRoute
![.NET Core Build and Test](https://github.com/JackWFinlay/EscapeRoute/workflows/.NET%20Core%20Build%20and%20Test/badge.svg)
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
- This project uses `Span<T>` under the hood to [speed up execution time and lower allocations and GC events](#benchmarks).
  but there are changes to the `EscapeRouteConfiguration` that must be used. 
  See [EscapeRoute configuration](#escaperoute-configuration) for details.
  
- The current characters that version `0.0.3` can escape is limited to the same that the version `0.0.2`
can handle, with the addition of `\r` Carriage Return characters. There are plans to expand this to some of the other special characters, please raise an 
  [issue](https://github.com/JackWFinlay/EscapeRoute/issues) for any requests.

## Supported behaviors
### EscapeRoute `0.0.3` Behaviors
The version `0.0.3` `EscapeRouter` currently supports the following behaviors/special characters (***Default***):

- Tab (\t):
    - ***Strip***
    - Escape
    - Ignore
- Carriage Return (\r):
    - ***Strip***
    - Escape
  - Ignore
- New line (\n):
    - Strip
    - Space
    - ***Escape***
    - Ignore
- Backspace (\b):
    - ***Strip***
    - Escape
    - Ignore
- Form feed (\f):
    - ***Strip***
    - Escape
    - Ignore
- Backslash (\\\\):
    - Strip
    - ***Escape***
    - Ignore
- Unicode (\u1234):
    - Strip
    - ***Escape***
    - Ignore
- Single quotes '':
    - ***Single***
    - Double
    - Ignore
- Double quotes "":
    - ***Double***
    - Single
    - Ignore
- Unicode Null (\0):
    - ***Strip***
    - Escape
    - EscapeHex
    - Ignore
- Unicode Surrogates:
    - ***Escape***
    - Strip
    - Ignore

Note that for New Line type, the default is different (***Strip***) for version `0.0.3`.
The `NewLineBehavior` enum now controls the handling of New Line characters `\n`.
This is due to the addition of a behavior handler for Carriage Return `\r` characters separately.

Also note that the trim behaviour is not available for version `0.0.3`, this may change in future -
feel free to request this under [issues](https://github.com/JackWFinlay/EscapeRoute/issues).

## Usage
### Namespace
Use the namespace `EscapeRoute`:

```C#
using EscapeRoute;
```

### Parsing

`EscapeRoute.SpanEngine` uses multiple overloads of the `ParseAsync` method to perform parsing of
either a `TextReader` or a `string`. You must provide the `TextReader` (`StreamReader`, `StringReader` etc.)
and load the file yourself.

Version `0.0.3` doesn't accept file paths.

test file: [unicode1.txt](EscapeRoute.Test/test-files/unicode1.txt)
```c#
using EscapeRoute;

namespace Example
{
    public async Task TestUnicodeFromFileAsyncDefaultParse()
    {
        string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
        IEscapeRouter escapeRouter = new EscapeRouter();
        const string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
        using StreamReader reader = File.OpenText(fileLocation);
        string result = await escapeRouter.ParseAsync(reader);
        Assert.Equal(expected, result); // True
    }
    
    public async Task TestUnicodeFromStringAsyncDefaultParse()
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
EscapeRoute allows configuration using an [`EscapeRouteConfiguration`](EscapeRoute/EscapeRouteConfiguration.cs) object,
which is passed to the EscapeRoute constructor:
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

A limitation of standard JSON is that characters represented as surrogate pairs must be represented as two consecutive escaped unicode characters.
e.g. `😍` becomes `\ud83d\ude0d`.

#### EscapeRoute Configuration
The configuration for the version `0.0.3` `EscapeRouter` is a bit different to that of the basic `0.0.2` `EscapeRouter`.
You can override the default behavior handling of any of the default behaviors
by supplying a class using the [`IEscapeRouteEscapeHandler`](EscapeRoute.SpanEngine.Abstractions/Interfaces/IEscapeRouteEscapeHandler.cs) interface.
See the [EscapeHandlers](EscapeRoute.SpanEngine/EscapeHandlers) for examples.

## Benchmarks
```
BenchmarkDotNet=v0.12.1, OS=macOS 11.3.1 (20E241) [Darwin 20.4.0]
Intel Core i5-1038NG7 CPU 2.00GHz, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.202
[Host]     : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT
DefaultJob : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT

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

|                            Method |     Mean |   Error |  StdDev | Ratio |    Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------- |---------:|--------:|--------:|------:|---------:|------:|------:|----------:|
|           AsciiParseAsync (0.0.2) | 320.7 μs | 6.32 μs | 6.76 μs |  1.00 | 130.3711 |     - |     - | 399.92 KB |
| AsciiParseAsyncSpanString (0.0.3) | 110.2 μs | 1.23 μs | 1.09 μs |  0.34 |  20.6299 |     - |     - |  63.41 KB |

|                                   Method |     Mean |   Error |  StdDev | Ratio |    Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------------------- |---------:|--------:|--------:|------:|---------:|------:|------:|----------:|
|           AsciiUnicodeParseAsync (0.0.2) | 608.6 μs | 8.11 μs | 7.19 μs |  1.00 | 221.6797 |     - |     - | 681.75 KB |
| AsciiUnicodeParseAsyncSpanString (0.0.3) | 197.7 μs | 2.09 μs | 1.63 μs |  0.32 |  55.1758 |     - |     - | 169.84 KB |

|                            Method |     Mean |    Error |   StdDev | Ratio |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------- |---------:|---------:|---------:|------:|--------:|------:|------:|----------:|
|           EmojiParseAsync (0.0.2) | 69.13 μs | 0.253 μs | 0.212 μs |  1.00 | 22.8271 |     - |     - |  70.12 KB |
| EmojiParseAsyncSpanString (0.0.3) | 24.83 μs | 0.086 μs | 0.071 μs |  0.36 |  7.7515 |     - |     - |  23.82 KB |

As shown by the results above, version `0.0.3` using .NET Standard 2.1 and above (e.g. .NET Core 3.0 & 3.1, .NET5) is more than twice as fast,
allocates considerably less memory, and performs far less GC events for the same given
[test data](EscapeRoute.Benchmarks.NET5/Benchmarks/Constants.cs).

## License
MIT

See [LICENSE](LICENSE) for details.
