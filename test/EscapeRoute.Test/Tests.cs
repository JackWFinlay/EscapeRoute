using JackWFinlay.EscapeRoute;
using System;
using Xunit;

namespace JackWFinlay.EscapeRoute.Test
{
    public class Tests
    {
        public String workspaceFolder = "REPLACE WITH LOCAL FOLDER ROOT TO RUN TESTS";

        [Fact]
        public async void Test1()
        {
            String fileLocation = $"{workspaceFolder}/test-files/test1.txt";
            EscapeRoute escapeRoute = new EscapeRoute();
            String result = await escapeRoute.ParseFileAsync(fileLocation);
            Assert.Equal(result, @"{\"node\": \"Document\",\"child\": [{\"node\": \"Comment\",\"text\": \"<!DOCTYPE html>\"},{\"node\": \"Element\",\"tag\": \"html\",\"child\": [{\"node\": \"Element\",\"tag\": \"body\",\"child\": [{\"node\": \"Element\",\"tag\": \"p\"},{\"node\": \"Text\",\"text\": \"test\"},{\"node\": \"Element\",\"tag\": \"p\"}]}]}]}");
        }
    }
}
