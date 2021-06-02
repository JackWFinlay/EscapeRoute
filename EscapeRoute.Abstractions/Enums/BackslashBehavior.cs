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
        Strip,

        /// <summary>
        /// Escapes backslash \\ characters.
        /// </summary>
        Escape,
        
        /// <summary>
        /// Ignore backslash \\ characters. Leave them as-is.
        /// </summary>
        Ignore
    }
}