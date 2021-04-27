using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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

        [Obsolete]
        [Fact]
        public void TestDefaultBehaviourFromFile()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            IEscapeRouter escapeRouter = new EscapeRouter();
            const string expected = @"\\The quick brown fox jumps over the lazy dog.";
            string result = escapeRouter.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public async void TestDefaultBehaviourFromFileAsync()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            IEscapeRouter escapeRouter = new EscapeRouter();
            const string expected = @"\\The quick brown fox jumps over the lazy dog.";
            string result = await escapeRouter.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async void TestDefaultBehaviourFromFileParseAsync()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            IEscapeRouter escapeRouter = new EscapeRouter();
            const string expected = @"\\The quick brown fox jumps over the lazy dog.";
            using StreamReader reader = File.OpenText(fileLocation);
            string result = await escapeRouter.ParseAsync(reader);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public void TestEscapeAllBehaviourNewLineSpaceFromFile()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Space
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"\\The quick  \tbrown fox jumps  \tover the lazy dog.";
            string result = escapeRouter.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }
        
        [Obsolete]
        [Fact]
        public void TestEscapeAllBehaviourNewLineNoneFromFile()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Strip
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"\\The quick \tbrown fox jumps \tover the lazy dog.";
            string result = escapeRouter.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }
        
        [Obsolete]
        [Fact]
        public void TestEscapeAllBehaviourNewLineUnixFromFile()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            var config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Unix
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"\\The quick \n\tbrown fox jumps \n\tover the lazy dog.";
            string result = escapeRouter.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }
        
        [Obsolete]
        [Fact]
        public void TestEscapeAllBehaviourNewLineWindowsFromFile()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Windows
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"\\The quick \r\n\tbrown fox jumps \r\n\tover the lazy dog.";
            string result = escapeRouter.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public async Task TestEscapeAllBehaviourNewLineSpaceFromFileAsync()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Space
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"\\The quick  \tbrown fox jumps  \tover the lazy dog.";
            using StreamReader reader = File.OpenText(fileLocation);
            string result = await escapeRouter.ParseAsync(reader);
            Assert.Equal(expected, result);
        }
        
        [Obsolete]
        [Fact]
        public async Task TestEscapeAllBehaviourNewLineNoneFromFileAsync()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Strip
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"\\The quick \tbrown fox jumps \tover the lazy dog.";
            using StreamReader reader = File.OpenText(fileLocation);
            string result = await escapeRouter.ParseAsync(reader);
            Assert.Equal(expected, result);
        }
        
        [Obsolete]
        [Fact]
        public async Task TestEscapeAllBehaviourNewLineUnixFromFileAsync()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            var config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Unix
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"\\The quick \n\tbrown fox jumps \n\tover the lazy dog.";
            using StreamReader reader = File.OpenText(fileLocation);
            string result = await escapeRouter.ParseAsync(reader);
            Assert.Equal(expected, result);
        }
        
        [Obsolete]
        [Fact]
        public async Task TestEscapeAllBehaviourNewLineWindowsFromFileAsync()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Windows
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"\\The quick \r\n\tbrown fox jumps \r\n\tover the lazy dog.";
            using StreamReader reader = File.OpenText(fileLocation);
            string result = await escapeRouter.ParseAsync(reader);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestEscapeAllBehaviourNewLineSpaceFromFileParseAsync()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Space
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"\\The quick  \tbrown fox jumps  \tover the lazy dog.";
            using StreamReader reader = File.OpenText(fileLocation);
            string result = await escapeRouter.ParseAsync(reader);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestEscapeAllBehaviourNewLineNoneFromFileParseAsync()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Strip
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"\\The quick \tbrown fox jumps \tover the lazy dog.";
            using StreamReader reader = File.OpenText(fileLocation);
            string result = await escapeRouter.ParseAsync(reader);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestEscapeAllBehaviourNewLineUnixFromFileParseAsync()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            var config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Unix
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"\\The quick \n\tbrown fox jumps \n\tover the lazy dog.";
            using StreamReader reader = File.OpenText(fileLocation);
            string result = await escapeRouter.ParseAsync(reader);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestEscapeAllBehaviourNewLineWindowsFromFileParseAsync()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/test1.txt";
            var config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Windows
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"\\The quick \r\n\tbrown fox jumps \r\n\tover the lazy dog.";
            using StreamReader reader = File.OpenText(fileLocation);
            string result = await escapeRouter.ParseAsync(reader);
            Assert.Equal(expected, result);
        }

        #region UnicodeFromFileTests

        [Obsolete]
        [Fact]
        public void TestUnicodeFromFileDefault()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
            IEscapeRouter escapeRouter = new EscapeRouter();
            const string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = escapeRouter.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public async void TestUnicodeFromFileAsyncDefault()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
            IEscapeRouter escapeRouter = new EscapeRouter();
            const string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRouter.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public void TestUnicodeFromFileExplicitEscape()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Escape
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = escapeRouter.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public async void TestUnicodeFromFileAsyncExplicitEscape()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Escape
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRouter.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public void TestUnicodeFromFileStrip()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Strip
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"(   )";
            string result = escapeRouter.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public async Task TestUnicodeFromFileAsyncStrip()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Strip
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"(   )";
            string result = await escapeRouter.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async void TestUnicodeFromFileAsyncDefaultParse()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
            IEscapeRouter escapeRouter = new EscapeRouter();
            const string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            using StreamReader reader = File.OpenText(fileLocation);
            string result = await escapeRouter.ParseAsync(reader);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestUnicodeFromFileAsyncExplicitEscapeParse()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Escape
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            using StreamReader reader = File.OpenText(fileLocation);
            string result = await escapeRouter.ParseAsync(reader);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task TestUnicodeFromFileAsyncStripParse()
        {
            string fileLocation = $"{_workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Strip
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"(   )";
            using StreamReader reader = File.OpenText(fileLocation);
            string result = await escapeRouter.ParseAsync(reader);
            Assert.Equal(expected, result);
        }

        #endregion UnicodeFromFileTests

        #endregion FromFileTests

        #region FromStringTests

        private const string _inputString1 = "The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog.";
        private const string _unicodeString1 = "( ͡° ͜ʖ ͡°)";

        [Obsolete]
        [Fact]
        public void TestDefaultBehaviourFromString()
        {
            IEscapeRouter escapeRouter = new EscapeRouter();
            const string expected = "The quick brown fox jumps over the lazy dog.";
            string result = escapeRouter.ParseString(_inputString1);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public async Task TestDefaultBehaviourFromStringAsync()
        {
            IEscapeRouter escapeRouter = new EscapeRouter();
            const string expected = "The quick brown fox jumps over the lazy dog.";
            string result = await escapeRouter.ParseStringAsync(_inputString1);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public void TestEscapeAllBehaviourNewLineNoneFromString()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Strip
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"The quick \t\bbrown fox jumps \t\bover the lazy dog.";
            string result = escapeRouter.ParseString(_inputString1);
            Assert.Equal(expected, result);
        }
        
        [Obsolete]
        [Fact]
        public void TestEscapeAllBehaviourNewLineSpaceFromString()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Space
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"The quick  \t\bbrown fox jumps  \t\bover the lazy dog.";
            string result = escapeRouter.ParseString(_inputString1);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public async Task TestEscapeAllBehaviourWindowsFromStringAsync()
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
            string result = await escapeRouter.ParseStringAsync(_inputString1);
            Assert.Equal(expected, result);
        }
        
        [Obsolete]
        [Fact]
        public async Task TestEscapeAllBehaviourUnixFromStringAsync()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Unix
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"The quick \n\t\bbrown fox jumps \n\t\bover the lazy dog.";
            string result = await escapeRouter.ParseStringAsync(_inputString1);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestDefaultBehaviourFromStringAsyncParse()
        {
            IEscapeRouter escapeRouter = new EscapeRouter();
            const string expected = "The quick brown fox jumps over the lazy dog.";
            string result = await escapeRouter.ParseAsync(_inputString1);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestEscapeAllBehaviourWindowsFromStringAsyncParse()
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
            string result = await escapeRouter.ParseAsync(_inputString1);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestEscapeAllBehaviourUnixFromStringAsyncParse()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehavior = TabBehavior.Escape,
                BackspaceBehavior = BackspaceBehavior.Escape,
                TrimBehavior = TrimBehavior.None,
                NewLineType = NewLineType.Unix
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"The quick \n\t\bbrown fox jumps \n\t\bover the lazy dog.";
            string result = await escapeRouter.ParseAsync(_inputString1);
            Assert.Equal(expected, result);
        }

        #region UnicodeFromStringTests

        [Obsolete]
        [Fact]
        public void TestUnicodeFromStringDefault()
        {
            IEscapeRouter escapeRouter = new EscapeRouter();
            const string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = escapeRouter.ParseString(_unicodeString1);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public async Task TestUnicodeFromStringAsyncDefault()
        {
            IEscapeRouter escapeRouter = new EscapeRouter();
            const string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRouter.ParseStringAsync(_unicodeString1);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestUnicodeFromStringAsyncDefaultParse()
        {
            IEscapeRouter escapeRouter = new EscapeRouter();
            const string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRouter.ParseAsync(_unicodeString1);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public void TestUnicodeFromStringExplicitEscape()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Escape
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = escapeRouter.ParseString(_unicodeString1);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public async Task TestUnicodeFromStringAsyncExplicitEscape()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Escape
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRouter.ParseStringAsync(_unicodeString1);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestUnicodeFromStringAsyncExplicitEscapeParse()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Escape
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRouter.ParseAsync(_unicodeString1);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public void TestUnicodeFromStringStrip()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Strip
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"(   )";
            string result = escapeRouter.ParseString(_unicodeString1);
            Assert.Equal(expected, result);
        }

        [Obsolete]
        [Fact]
        public async Task TestUnicodeFromStringAsyncStrip()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Strip
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"(   )";
            string result = await escapeRouter.ParseStringAsync(_unicodeString1);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestUnicodeFromStringAsyncStripParse()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehavior = UnicodeBehavior.Strip
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = @"(   )";
            string result = await escapeRouter.ParseAsync(_unicodeString1);
            Assert.Equal(expected, result);
        }

        #endregion UnicodeFromStringTests

        #region CustomhandlersTests
        private class ExampleCustomBehaviorHandler : IEscapeRouteCustomBehaviorHandler
        {
            public Task<string> EscapeAsync(string raw)
            {
                // Replace occurrences of string "dog" with string "cat", in string raw.
                string escaped = Regex.Replace(raw, "dog", "cat");

                return Task.FromResult(escaped);
            }
        }
        
        [Obsolete]
        [Fact]
        public void TestCustomHandlerBehaviourFromString()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration()
            {
                CustomBehaviorHandlers = new List<IEscapeRouteCustomBehaviorHandler>()
                {
                    new ExampleCustomBehaviorHandler()
                }
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = "The quick brown fox jumps over the lazy cat.";
            string result = escapeRouter.ParseString(_inputString1);
            Assert.Equal(expected, result);
        }
        
        [Obsolete]
        [Fact]
        public void TestCustomHandlerBehaviourEmptyListFromString()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration()
            {
                CustomBehaviorHandlers = new List<IEscapeRouteCustomBehaviorHandler>()
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = "The quick brown fox jumps over the lazy dog.";
            string result = escapeRouter.ParseString(_inputString1);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestCustomHandlerBehaviourFromStringParse()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration()
            {
                CustomBehaviorHandlers = new List<IEscapeRouteCustomBehaviorHandler>()
                {
                    new ExampleCustomBehaviorHandler()
                }
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = "The quick brown fox jumps over the lazy cat.";
            string result = await escapeRouter.ParseAsync(_inputString1);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public async Task TestCustomHandlerBehaviourEmptyListFromStringParse()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration()
            {
                CustomBehaviorHandlers = new List<IEscapeRouteCustomBehaviorHandler>()
            };
            IEscapeRouter escapeRouter = new EscapeRouter(config);
            const string expected = "The quick brown fox jumps over the lazy dog.";
            string result = await escapeRouter.ParseAsync(_inputString1);
            Assert.Equal(expected, result);
        }

        #endregion CustomhandlersTests
        
        #endregion FromStringTests

        [Fact]
        public async Task TestSpanReplacementEngine()
        {
            IEscapeRouter escapeRouter = new SpanEngine.EscapeRouter();
            const string inputString = "The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog. ( ͡° ͜ʖ ͡°)";
            const string expected = @"The quick brown fox jumps over the lazy dog. ( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRouter.ParseAsync(inputString);
            Assert.Equal(expected, result);
        }
    }
}
