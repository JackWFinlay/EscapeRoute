using JackWFinlay.EscapeRoute;
using System;
using System.IO;
using Xunit;

namespace JackWFinlay.EscapeRoute.Test
{
    public class Tests
    {

        #region FromFileTests

        internal readonly static string workspaceFolder = "../../..";

        [Fact]
        public void TestDefaultBehaviourFromFile()
        {
            string fileLocation = $"{workspaceFolder}/test-files/test1.txt";
            IEscapeRoute escapeRoute = new EscapeRoute();
            string expected = @"\\The quick brown fox jumps over the lazy dog.";
            string result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestDefaultBehaviourFromFileAsync()
        {
            string fileLocation = $"{workspaceFolder}/test-files/test1.txt";
            IEscapeRoute escapeRoute = new EscapeRoute();
            string expected = @"\\The quick brown fox jumps over the lazy dog.";
            string result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestEscapeAllBehaviourFromFile()
        {
            string fileLocation = $"{workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehaviour = TabBehaviour.Escape,
                NewLineBehaviour = NewLineBehaviour.Escape,
                CarriageReturnBehaviour = CarriageReturnBehaviour.Escape,
                BackspaceBehaviour = BackspaceBehaviour.Escape,
                TrimBehaviour = TrimBehaviour.None
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            string expected = @"\\The quick \n\tbrown fox jumps \n\tover the lazy dog.";
            string result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestEscapeAllBehaviourFromFileAsync()
        {
            string fileLocation = $"{workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehaviour = TabBehaviour.Escape,
                NewLineBehaviour = NewLineBehaviour.Escape,
                CarriageReturnBehaviour = CarriageReturnBehaviour.Escape,
                BackspaceBehaviour = BackspaceBehaviour.Escape,
                TrimBehaviour = TrimBehaviour.None
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            string expected = @"\\The quick \n\tbrown fox jumps \n\tover the lazy dog.";
            string result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        #region UnicodeFromFileTests

        [Fact]
        public void TestUnicodeFromFileDefault()
        {
            string fileLocation = $"{workspaceFolder}/test-files/unicode1.txt";
            IEscapeRoute escapeRoute = new EscapeRoute();
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestUnicodeFromFileAsyncDefault()
        {
            string fileLocation = $"{workspaceFolder}/test-files/unicode1.txt";
            IEscapeRoute escapeRoute = new EscapeRoute();
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestUnicodeFromFileExplicitEscape()
        {
            string fileLocation = $"{workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehaviour = UnicodeBehaviour.Escape
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestUnicodeFromFileAsyncExplicitEscape()
        {
            string fileLocation = $"{workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehaviour = UnicodeBehaviour.Escape
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestUnicodeFromFileStrip()
        {
            string fileLocation = $"{workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehaviour = UnicodeBehaviour.Strip
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            string expected = @"(   )";
            string result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestUnicodeFromFileAsyncStrip()
        {
            string fileLocation = $"{workspaceFolder}/test-files/unicode1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                UnicodeBehaviour = UnicodeBehaviour.Strip
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
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
            IEscapeRoute escapeRoute = new EscapeRoute();
            string expected = "The quick brown fox jumps over the lazy dog.";
            string result = escapeRoute.ParseString(inputString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestDefaultBehaviourFromStringAsync()
        {
            IEscapeRoute escapeRoute = new EscapeRoute();
            string expected = "The quick brown fox jumps over the lazy dog.";
            string result = await escapeRoute.ParseStringAsync(inputString1);
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
            string expected = @"The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog.";
            string result = escapeRoute.ParseString(inputString1);
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
            string expected = @"The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog.";
            string result = await escapeRoute.ParseStringAsync(inputString1);
            Assert.Equal(expected, result);
        }

        #region UnicodeFromStringTests

        [Fact]
        public void TestUnicodeFromStringDefault()
        {
            IEscapeRoute escapeRoute = new EscapeRoute();
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = escapeRoute.ParseString(unicodeString1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestUnicodeFromStringAsyncDefault()
        {
            IEscapeRoute escapeRoute = new EscapeRoute();
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRoute.ParseStringAsync(unicodeString1);
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
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = escapeRoute.ParseString(unicodeString1);
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
            string expected = @"( \u0361\u00b0 \u035c\u0296 \u0361\u00b0)";
            string result = await escapeRoute.ParseStringAsync(unicodeString1);
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
            string expected = @"(   )";
            string result = escapeRoute.ParseString(unicodeString1);
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
            string expected = @"(   )";
            string result = await escapeRoute.ParseStringAsync(unicodeString1);
            Assert.Equal(expected, result);
        }

        #endregion UnicodeFromStringTests

        #endregion FromStringTests
    }
}
