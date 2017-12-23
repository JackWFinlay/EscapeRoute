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
            String expected = "The quick brown fox jumps over the lazy dog.";
            String result = escapeRoute.ParseFile(fileLocation);
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestDefaultBehaviourFromFileAsync()
        {
            String fileLocation = $"{workspaceFolder}/test-files/test1.txt";
            IEscapeRoute escapeRoute = new EscapeRoute();
            String expected = "The quick brown fox jumps over the lazy dog.";
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
            String expected = @"The quick \n\tbrown fox jumps \n\tover the lazy dog.";
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
            String expected = @"The quick \n\tbrown fox jumps \n\tover the lazy dog.";
            String result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(expected, result);
        }

        #endregion FromFileTests

        #region FromStringTests

        internal readonly static String inputString1 = "The quick \r\n\t\bbrown fox jumps \r\n\t\bover the lazy dog.";

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

        #endregion FromStringTests
    }
}
