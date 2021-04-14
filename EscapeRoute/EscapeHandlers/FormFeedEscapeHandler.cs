using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EscapeRoute.Abstractions.Enums;
using EscapeRoute.Abstractions.Interfaces;

namespace EscapeRoute.EscapeHandlers
{
    public class FormFeedEscapeHandler : IEscapeRouteEscapeHandler<FormFeedBehavior>
    {
        private const char _pattern = '\f';
        private readonly ReadOnlyMemory<char> _replacePattern = new char[] {'\\', 'f'};
        private readonly ReadOnlyMemory<char> _stripPattern = new char[] {};

        public char GetPattern()
        {
            return _pattern;
        }

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