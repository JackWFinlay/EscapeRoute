using System;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.EscapeHandlers
{
    public class FormFeedEscapeHandler : IEscapeRouteEscapeHandler<FormFeedBehavior>
    {
        private const char _pattern = '\f';
        private readonly ReadOnlyMemory<char> _replacePattern = new[] {'\\', 'f'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};
        private readonly ReadOnlyMemory<char> _ignorePattern = new[] { _pattern };

        public char GetPattern() => _pattern;

        public ReadOnlyMemory<char> GetReplacement(FormFeedBehavior behavior)
        {
            var escaped = behavior switch
            {
                FormFeedBehavior.Escape => _replacePattern,
                FormFeedBehavior.Strip => _stripPattern,
                FormFeedBehavior.Ignore => _ignorePattern,
                _ => throw new ArgumentException($"Not a valid {nameof(FormFeedBehavior)}", nameof(behavior))
            };

            return escaped;
        }
    }
}