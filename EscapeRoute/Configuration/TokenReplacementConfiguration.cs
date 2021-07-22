using System;
using System.Collections.Generic;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.Configuration
{
    public class TokenReplacementConfiguration
    {
        public ReadOnlyMemory<char> TokenStart { get; init; }
        public ReadOnlyMemory<char> TokenEnd { get; init; }
        public IDictionary<ReadOnlyMemory<char>, ReadOnlyMemory<char>> SubstitutionMap { get; init; }
    }
}