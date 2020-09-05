using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;
using Xunit;

namespace EscapeRoute.Test
{
    public class Tests
    {

        #region FromFileTests

        private const string _workspaceFolder = "../../..";

        [Fact]
        public void TestDefaultBehaviourFromFile()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            IEscapeRoute escapeRoute = new EscapeRouter();
            string expected = @"\\The quick brown fox jumps over the lazy dog.";
            string result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestDefaultBehaviourFromFileAsync()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            IEscapeRoute escapeRoute = new EscapeRouter();
            string expected = @"\\The quick brown fox jumps over the lazy dog.";
            string result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestEscapeAllBehaviourNewLineSpaceFromFile()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                NewLineBehavior = NewLineBehavior.Escape,
                CarriageReturnBehavior = CarriageReturnBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Space
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"\\The quick  \tbrown fox jumps  \tover the lazy dog.";
            string result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void TestEscapeAllBehaviourNewLineNoneFromFile()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                NewLineBehavior = NewLineBehavior.Escape,
                CarriageReturnBehavior = CarriageReturnBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"\\The quick \tbrown fox jumps \tover the lazy dog.";
            string result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void TestEscapeAllBehaviourNewLineUnixFromFile()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                NewLineBehavior = NewLineBehavior.Escape,
                CarriageReturnBehavior = CarriageReturnBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Unix
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"\\The quick \n\tbrown fox jumps \n\tover the lazy dog.";
            string result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void TestEscapeAllBehaviourNewLineWindowsFromFile()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                NewLineBehavior = NewLineBehavior.Escape,
                CarriageReturnBehavior = CarriageReturnBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Windows
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"\\The quick \r\n\tbrown fox jumps \r\n\tover the lazy dog.";
            string result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task TestEscapeAllBehaviourNewLineSpaceFromFileAsync()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                NewLineBehavior = NewLineBehavior.Escape,
                CarriageReturnBehavior = CarriageReturnBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Space
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"\\The quick  \tbrown fox jumps  \tover the lazy dog.";
            string result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestEscapeAllBehaviourNewLineNoneFromFileAsync()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                NewLineBehavior = NewLineBehavior.Escape,
                CarriageReturnBehavior = CarriageReturnBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"\\The quick \tbrown fox jumps \tover the lazy dog.";
            string result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestEscapeAllBehaviourNewLineUnixFromFileAsync()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                NewLineBehavior = NewLineBehavior.Escape,
                CarriageReturnBehavior = CarriageReturnBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Unix
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"\\The quick \n\tbrown fox jumps \n\tover the lazy dog.";
            string result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestEscapeAllBehaviourNewLineWindowsFromFileAsync()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                NewLineBehavior = NewLineBehavior.Escape,
                CarriageReturnBehavior = CarriageReturnBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Windows
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"\\The quick \r\n\tbrown fox jumps \r\n\tover the lazy dog.";
            string result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        #region UnicodeFromFileTests

        [Fact]
        public void TestUnicodeFromFileDefault()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
            IEscapeRoute escapeRoute = new EscapeRouter();
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestUnicodeFromFileAsyncDefault()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
            IEscapeRoute escapeRoute = new EscapeRouter();
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestUnicodeFromFileExplicitEscape()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Escape
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestUnicodeFromFileAsyncExplicitEscape()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Escape
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestUnicodeFromFileStrip()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Strip
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"(   )";
            string result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task TestUnicodeFromFileAsyncStrip()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Strip
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"(   )";
            string result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        #endregion UnicodeFromFileTests

        #endregion FromFileTests

        #region FromStringTests

        internal readonly static string inputString1 = "The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog.";
        internal readonly static string unicodeString1 = "( ͡° ͜ʖ ͡°)";

        [Fact]
        public void TestDefaultBehaviourFromString()
        {
            IEscapeRoute escapeRoute = new EscapeRouter();
            string expected = "The quick brown fox jumps over the lazy dog.";
            string result = escapeRoute.ParseString(inputString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task TestDefaultBehaviourFromStringAsync()
        {
            IEscapeRoute escapeRoute = new EscapeRouter();
            string expected = "The quick brown fox jumps over the lazy dog.";
            string result = await escapeRoute.ParseStringAsync(inputString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestEscapeAllBehaviourNewLineNoneFromString()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                NewLineBehavior = NewLineBehavior.Escape,
                CarriageReturnBehavior = CarriageReturnBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.None
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"The quick \t\bbrown fox jumps \t\bover the lazy dog.";
            string result = escapeRoute.ParseString(inputString1);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void TestEscapeAllBehaviourNewLineSpaceFromString()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                NewLineBehavior = NewLineBehavior.Escape,
                CarriageReturnBehavior = CarriageReturnBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Space
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"The quick  \t\bbrown fox jumps  \t\bover the lazy dog.";
            string result = escapeRoute.ParseString(inputString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task TestEscapeAllBehaviourWindowsFromStringAsync()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                NewLineBehavior = NewLineBehavior.Escape,
                CarriageReturnBehavior = CarriageReturnBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Windows
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog.";
            string result = await escapeRoute.ParseStringAsync(inputString1);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestEscapeAllBehaviourUnixFromStringAsync()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                NewLineBehavior = NewLineBehavior.Escape,
                CarriageReturnBehavior = CarriageReturnBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Unix
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"The quick \n\t\bbrown fox jumps \n\t\bover the lazy dog.";
            string result = await escapeRoute.ParseStringAsync(inputString1);
            Assert.Equal(expected, result);
        }

        #region UnicodeFromStringTests

        [Fact]
        public void TestUnicodeFromStringDefault()
        {
            IEscapeRoute escapeRoute = new EscapeRouter();
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = escapeRoute.ParseString(unicodeString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task TestUnicodeFromStringAsyncDefault()
        {
            IEscapeRoute escapeRoute = new EscapeRouter();
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRoute.ParseStringAsync(unicodeString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestUnicodeFromStringExplicitEscape()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Escape
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = escapeRoute.ParseString(unicodeString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task TestUnicodeFromStringAsyncExplicitEscape()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Escape
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRoute.ParseStringAsync(unicodeString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestUnicodeFromStringStrip()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Strip
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"(   )";
            string result = escapeRoute.ParseString(unicodeString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task TestUnicodeFromStringAsyncStrip()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Strip
            };
            IEscapeRoute escapeRoute = new EscapeRouter(config);
            string expected = @"(   )";
            string result = await escapeRoute.ParseStringAsync(unicodeString1);
            Assert.Equal(expected, result);
        }

        #endregion UnicodeFromStringTests

        #endregion FromStringTests
    }
}
