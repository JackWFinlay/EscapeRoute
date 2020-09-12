namespace EscapeRoute.Abstractions.Enums
{
    /// <summary>
    /// Specifies trim behavior. Operates per line of text.
    /// </summary>
    public enum TrimBehavior
    {
        /// <summary>
        /// Trims start of string.
        /// </summary>
        None = 0,

        /// <summary>
        /// Trims start of string.
        /// </summary>
        Start = 1,

        /// <summary>
        /// Trims end of string.
        /// </summary>
        End = 2,

        /// <summary>
        /// Trims both start and end of string.
        /// </summary>
        Both = 3,
    }
}