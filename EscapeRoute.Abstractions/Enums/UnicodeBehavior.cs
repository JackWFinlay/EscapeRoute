namespace EscapeRoute.Abstractions.Enums
{
    /// <summary>
    /// Specifies handling of unicode \uXXXX characters (non-ASCII).
    /// </summary>
    public enum UnicodeBehavior
    {
        /// <summary>
        /// Strips unicode \uXXXX characters.
        /// </summary>
        Strip = 0,

        /// <summary>
        /// Escapes unicode \uXXXX characters.
        /// </summary>
        Escape = 1
    }
}