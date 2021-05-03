namespace EscapeRoute.SpanEngine.Abstractions.Enums
{
    public enum UnicodeNullBehavior
    {
        /// <summary>
        /// Strips out Unicode Null characters (\0)
        /// </summary>
        Strip = 0,
        /// <summary>
        /// Escapes Unicode Null characters (\0), replacing them with the literal characters '\' and '0'
        /// </summary>
        Escape = 1,
        /// <summary>
        /// Escapes Unicode Null characters (\0), replacing them with the hex code Unicode escape sequence '\u0000'
        /// </summary>
        EscapeHex = 2,
    }
}