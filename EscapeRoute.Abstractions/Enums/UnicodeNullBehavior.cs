namespace EscapeRoute.Abstractions.Enums
{
    public enum UnicodeNullBehavior
    {
        /// <summary>
        /// Strips out Unicode Null characters (\0).
        /// </summary>
        Strip,
        
        /// <summary>
        /// Escapes Unicode Null characters (\0), replacing them with the literal characters '\' and '0'.
        /// </summary>
        Escape,
        
        /// <summary>
        /// Escapes Unicode Null characters (\0), replacing them with the hex code Unicode escape sequence '\u0000'.
        /// </summary>
        EscapeHex,
        
        /// <summary>
        /// Ignore Unicode Null characters (\0). Leave them as-is.
        /// </summary>
        Ignore
    }
}