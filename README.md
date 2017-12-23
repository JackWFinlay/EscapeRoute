# EscapeRoute
Selectively trim and escape text files into JSON friendly strings.

Currently suports the following behaviours/special characters (***Default***):

 - Tab (\t):
   - ***Strip***
   - Escape
 - New line (\n):
   - ***Strip***
   - Escape
 - Carriage return (\r):
   - ***Strip***
   - Escape
 - Backspace (\b):
   - ***Strip***
   - Escape
 - Single quotes '' (\\\'):
   - ***Escape***
 - Double quotes "" (\\\"):
   - ***Escape***
 - Trim
   - None
   - Start
   - End
   - ***Both***

## Usage
Use the namespace JackWFinlay.EscapeRoute:

```C#
using JackWFinlay.EscapeRoute;
```

EscapeRoute allows the use of Files or Strings to load in the data to be escaped and trimmed. These can be called synchronously or asynchronously. 

E.g. `ParseFile` and `ParseStringAsync`

test file: [test1.txt](test/EscapeRoute.Test/test-files/test1.txt)
```C#
using JackWFinlay.EscapeRoute;

namespace Example
{
    public class ExampleProgram
    {
        public void TestDefaultBehaviourFromFile()
        {
            String fileLocation = $"{workspaceFolder}/test-files/test1.txt";
            IEscapeRoute escapeRoute = new EscapeRoute();
            String expected = "The quick brown fox jumps over the lazy dog.";

            String result = escapeRoute.ParseFile(fileLocation);
            String areEqual = expected.Equals(result) ? "" : " not";
            Console.WriteLine($"The strings are{areEqual} equal"); 
            // "The strings are equal"
        }
    }
}
```
EscapeRoute allows configuration using a `EscapeRouteConfiguration` object, which is passed to the EscapeRoute constructor:
```C#
namespace Example
{
    public class ExampleProgram
    {
        internal readonly static String inputString1 = 
            "The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog.";

        public async void TestEscapeAllBehaviourFromStringAsync()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehaviour = TabBehaviour.Escape,
                NewLineBehaviour = NewLineBehaviour.Escape,
                CarriageReturnBehaviour = CarriageReturnBehaviour.Escape,
                BackspaceBehaviour = BackspaceBehaviour.Escape,
                TrimBehaviour = TrimBehaviour.None
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            String expected = @"The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog.";
            String result = await escapeRoute.ParseStringAsync(inputString1);

            Console.WriteLine(result); 
            // "The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog."
            String areEqual = expected.Equals(result) ? "" : " not";
            Console.WriteLine($"The strings are{areEqual} equal"); 
            // "The strings are equal"
        }
    }
}
```

## License
MIT

See [license.md](license.md) for details.
