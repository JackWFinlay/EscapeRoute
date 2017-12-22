using JackWFinlay.EscapeRoute;
using System;
using Xunit;

namespace JackWFinlay.EscapeRoute.Test
{
    public class Tests
    {
        public String workspaceFolder = "REPLACE WITH PROJECT ROUTE";

        [Fact]
        public async void TestDefaultBehaviour()
        {
            String fileLocation = $"{workspaceFolder}/test-files/test1.txt";
            IEscapeRoute escapeRoute = new EscapeRoute();
            String result = await escapeRoute.ParseFileAsync(fileLocation);
            //String expected = "{\"node\": \"Document\",\"child\": [{\"node\": \"Comment\",\"text\": \"<!DOCTYPE html>\"},{\"node\": \"Element\",\"tag\": \"html\",\"child\": [{\"node\": \"Element\",\"tag\": \"body\",\"child\": [{\"node\": \"Element\",\"tag\": \"p\"},{\"node\": \"Text\",\"text\": \"test\"},{\"node\": \"Element\",\"tag\": \"p\"}]}]}]}";
            String expected = "{\\\"node\\\": \\\"Document\\\",\\\"child\\\": [{\\\"node\\\": \\\"Comment\\\",\\\"text\\\": \\\"<!DOCTYPE html>\\\"},{\\\"node\\\": \\\"Element\\\",\\\"tag\\\": \\\"html\\\",\\\"child\\\": [{\\\"node\\\": \\\"Element\\\",\\\"tag\\\": \\\"body\\\",\\\"child\\\": [{\\\"node\\\": \\\"Element\\\",\\\"tag\\\": \\\"p\\\"},{\\\"node\\\": \\\"Text\\\",\\\"text\\\": \\\"test\\\"},{\\\"node\\\": \\\"Element\\\",\\\"tag\\\": \\\"p\\\"}]}]}]}";
            Assert.Equal(expected, result);
        }

        [Fact]
        public async void TestEscapeAllbBehaviour()
        {
            String fileLocation = $"{workspaceFolder}/test-files/test1.txt";
            EscapeRouteConfiguration config = new EscapeRouteConfiguration
            {
                TabBehaviour = TabBehaviour.Escape,
                NewLineBehaviour = NewLineBehavior.Escape,
                CarriageReturnBehaviour = CarriageReturnBehaviour.Escape,
                BackspaceBehaviour = BackspaceBehaviour.Escape
            };
            IEscapeRoute escapeRoute = new EscapeRoute(config);
            String result = await escapeRoute.ParseFileAsync(fileLocation);
            // Fix the below expected output.
            String expected = "{\\\"node\\\": \\\"Document\\\",\\\"child\\\": [{\\\"node\\\": \\\"Comment\\\",\\\"text\\\": \\\"<!DOCTYPE html>\\\"},{\\\"node\\\": \\\"Element\\\",\\\"tag\\\": \\\"html\\\",\\\"child\\\": [{\\\"node\\\": \\\"Element\\\",\\\"tag\\\": \\\"body\\\",\\\"child\\\": [{\\\"node\\\": \\\"Element\\\",\\\"tag\\\": \\\"p\\\"},{\\\"node\\\": \\\"Text\\\",\\\"text\\\": \\\"test\\\"},{\\\"node\\\": \\\"Element\\\",\\\"tag\\\": \\\"p\\\"}]}]}]}";
            Assert.StrictEqual(expected, result);
        }
    }
}
