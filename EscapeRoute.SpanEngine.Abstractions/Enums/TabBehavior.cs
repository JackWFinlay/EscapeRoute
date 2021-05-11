namespace EscapeRoute.SpanEngine.Abstractions.Enums
{
    /// <summary>
    /// Specifies handling of tab \t characters.
    /// </summary>
    public enum TabBehavior
    {
        /// <summary>
        /// Strips tab \t characters.
        /// </summary>
        Strip,

        /// <summary>
        /// Escapes tab \t characters.
        /// </summary>
        Escape,
        
        /// <summary>
        /// Ignore tab \t characters. Leave them as-is.
        /// </summary>
        Ignore
    }
}