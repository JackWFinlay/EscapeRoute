using System.Threading.Tasks;
using EscapeRoute.Configuration;
using Xunit;

namespace EscapeRoute.Test.TokenEscapeTests
{
    public class TokenEscapeTests
    {
        [Fact]
        public async Task TestTokenReplacementEngine_MatchingStartEndTokens_ReturnsProperlySubstitutedText()
        {
            var config = new TokenReplacementConfigurationBuilder().SetTokenStart("{")
                                                                   .SetTokenEnd("}")
                
        }
    }
}