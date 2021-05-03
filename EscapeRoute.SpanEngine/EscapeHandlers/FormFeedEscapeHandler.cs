using System;
using EscapeRoute.SpanEngine.Abstractions.Enums;
using EscapeRoute.SpanEngine.Abstractions.Interfaces;

namespace EscapeRoute.SpanEngine.EscapeHandlers
{
    public class FormFeedEscapeHandler : IEscapeRouteEscapeHandler<FormFeedBehavior>
    {
        private const char _pattern = '\f';
        private readonly ReadOnlyMemory<char> _replacePattern = new[] {'\\', 'f'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};

        public char GetPattern() => _pattern;

        public ReadOnlyMemory<char> GetReplacement(FormFeedBehavior behavior)
        {
            var escaped = behavior switch
            {
                FormFeedBehavior.Escape => _replacePattern,
                FormFeedBehavior.Strip => _stripPattern,
                _ => throw new ArgumentException($"Not a valid {nameof(FormFeedBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}