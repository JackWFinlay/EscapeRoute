namespace EscapeRoute.Abstractions.Enums
{
    /// <summary>
    /// Specifies handling of backslash \\ characters.
    /// </summary>
    public enum BackslashBehavior
    {
        /// <summary>
        /// Strips backslash \\ characters.
        /// </summary>
        Strip = 0,

        /// <summary>
        /// Escapes backslash \\ characters.
        /// </summary>
        Escape = 1
    }
}