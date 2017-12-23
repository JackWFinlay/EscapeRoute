using JackWFinlay.EscapeRoute;
using System;
using System.IO;
using Xunit;

namespace JackWFinlay.EscapeRoute.Test
{
    public class Tests
    {

        #region FromFileTests

        internal readonly static String workspaceFolder = "../../..";

        [Fact]
        public void TestDefaultBehaviourFromFile()
        {
            String fileLocation = $"{workspaceFolder}/test-files/test1.txt";
            IEscapeRoute escapeRoute = new EscapeRoute();
            String expected = @"\\The quick brown fox jumps over the lazy dog.";
            String result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestDefaultBehaviourFromFileAsync()
        {
            String fileLocation = $"{workspaceFolder}/test-files/test1.txt";
            IEscapeRoute escapeRoute = new EscapeRoute();
            String expected = @"\\The quick brown fox jumps over the lazy dog.";
            String result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestEscapeAllBehaviourFromFile()
        {
            String fileLocation = $"{workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehaviour = TabBehaviour.Escape,
                NewLineBehaviour = NewLineBehaviour.Escape,
                CarriageReturnBehaviour = CarriageReturnBehaviour.Escape,
                BackspaceBehaviour = BackspaceBehaviour.Escape,
                TrimBehaviour = TrimBehaviour.None
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            String expected = @"\\The quick \n\tbrown fox jumps \n\tover the lazy dog.";
            String result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestEscapeAllBehaviourFromFileAsync()
        {
            String fileLocation = $"{workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehaviour = TabBehaviour.Escape,
                NewLineBehaviour = NewLineBehaviour.Escape,
                CarriageReturnBehaviour = CarriageReturnBehaviour.Escape,
                BackspaceBehaviour = BackspaceBehaviour.Escape,
                TrimBehaviour = TrimBehaviour.None
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            String expected = @"\\The quick \n\tbrown fox jumps \n\tover the lazy dog.";
            String result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        #region UnicodeFromFileTests

        [Fact]
        public void TestUnicodeFromFileDefault()
        {
            String fileLocation = $"{workspaceFolder}/test-files/unicode1.txt";
            IEscapeRoute escapeRoute = new EscapeRoute();
            String expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            String result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestUnicodeFromFileAsyncDefault()
        {
            String fileLocation = $"{workspaceFolder}/test-files/unicode1.txt";
            IEscapeRoute escapeRoute = new EscapeRoute();
            String expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            String result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestUnicodeFromFileExplicitEscape()
        {
            String fileLocation = $"{workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehaviour = UnicodeBehaviour.Escape
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            String expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            String result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestUnicodeFromFileAsyncExplicitEscape()
        {
            String fileLocation = $"{workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehaviour = UnicodeBehaviour.Escape
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            String expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            String result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestUnicodeFromFileStrip()
        {
            String fileLocation = $"{workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehaviour = UnicodeBehaviour.Strip
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            String expected = @"(   )";
            String result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestUnicodeFromFileAsyncStrip()
        {
            String fileLocation = $"{workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehaviour = UnicodeBehaviour.Strip
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            String expected = @"(   )";
            String result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        #endregion UnicodeFromFileTests

        #endregion FromFileTests

        #region FromStringTests

        internal readonly static String inputString1 = "The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog.";
        internal readonly static String unicodeString1 = "( ͡° ͜ʖ ͡°)";

        [Fact]
        public void TestDefaultBehaviourFromString()
        {
            IEscapeRoute escapeRoute = new EscapeRoute();
            String expected = "The quick brown fox jumps over the lazy dog.";
            String result = escapeRoute.ParseString(inputString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestDefaultBehaviourFromStringAsync()
        {
            IEscapeRoute escapeRoute = new EscapeRoute();
            String expected = "The quick brown fox jumps over the lazy dog.";
            String result = await escapeRoute.ParseStringAsync(inputString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestEscapeAllBehaviourFromString()
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
            String result = escapeRoute.ParseString(inputString1);
            Assert.Equal(expected, result);
        }

        [Fact]
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
            Assert.Equal(expected, result);
        }

        #region UnicodeFromStringTests

        [Fact]
        public void TestUnicodeFromStringDefault()
        {
            IEscapeRoute escapeRoute = new EscapeRoute();
            String expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            String result = escapeRoute.ParseString(unicodeString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestUnicodeFromStringAsyncDefault()
        {
            IEscapeRoute escapeRoute = new EscapeRoute();
            String expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            String result = await escapeRoute.ParseStringAsync(unicodeString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestUnicodeFromStringExplicitEscape()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehaviour = UnicodeBehaviour.Escape
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            String expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            String result = escapeRoute.ParseString(unicodeString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestUnicodeFromStringAsyncExplicitEscape()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehaviour = UnicodeBehaviour.Escape
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            String expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            String result = await escapeRoute.ParseStringAsync(unicodeString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestUnicodeFromStringStrip()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehaviour = UnicodeBehaviour.Strip
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            String expected = @"(   )";
            String result = escapeRoute.ParseString(unicodeString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestUnicodeFromStringAsyncStrip()
        {
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehaviour = UnicodeBehaviour.Strip
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            String expected = @"(   )";
            String result = await escapeRoute.ParseStringAsync(unicodeString1);
            Assert.Equal(expected, result);
        }

        #endregion UnicodeFromStringTests

        #endregion FromStringTests
    }
}
