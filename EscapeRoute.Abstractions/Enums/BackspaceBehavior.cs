namespace EscapeRoute.Abstractions.Enums
{
    /// <summary>
    /// Specifies handling of backspace \b characters.
    /// </summary>
    public enum BackspaceBehavior
    {
        /// <summary>
        /// Strips backspace \b characters.
        /// </summary>
        Strip,

        /// <summary>
        /// Escapes backspace \b characters.
        /// </summary>
        Escape,
        
        /// <summary>
        /// Ignores backspace \b characters. Leave them as-is.
        /// </summary>
        Ignore
    }
}