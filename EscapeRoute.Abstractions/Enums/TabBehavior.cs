namespace EscapeRoute.Abstractions.Enums
{
    /// <summary>
    /// Specifies handling of tab \t characters.
    /// </summary>
    public enum TabBehavior
    {
        /// <summary>
        /// Strips tab \t characters.
        /// </summary>
        Strip = 0,

        /// <summary>
        /// Escapes tab \t characters.
        /// </summary>
        Escape = 1
    }
}