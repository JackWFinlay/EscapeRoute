using JackWFinlay.EscapeRoute;
using System;
using System.IO;
using Xunit;

namespace JackWFinlay.EscapeRoute.Test
{
    public class Tests
    {
        public String workspaceFolder = "../../..";

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
    }
}
