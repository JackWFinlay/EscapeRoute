using System;

namespace EscapeRoute.Models
{
    internal class TokenMatch
    {
        public ReadOnlyMemory<char> Token { get; init; }
        public int Index { get; init; }
    }
}