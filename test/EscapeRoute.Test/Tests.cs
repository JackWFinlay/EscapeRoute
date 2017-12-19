using JackWFinlay.EscapeRoute;
using System;
using Xunit;

namespace JackWFinlay.EscapeRoute.Test
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            String fileLocation = "testfiles/test1.txt";
            EscapeRoute escapeRoute = new JackWFinlay.EscapeRoute();
            escapeRoute.ParseFileAsync(fileLocation);
        }
    }
}
