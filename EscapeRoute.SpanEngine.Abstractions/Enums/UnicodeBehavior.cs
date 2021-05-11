namespace EscapeRoute.SpanEngine.Abstractions.Enums
{
    /// <summary>
    /// Specifies handling of unicode \uXXXX characters (non-ASCII).
    /// </summary>
    public enum UnicodeBehavior
    {
        /// <summary>
        /// Strips unicode \uXXXX characters.
        /// </summary>
        Strip,

        /// <summary>
        /// Escapes unicode \uXXXX characters.
        /// </summary>
        Escape,
        
        /// <summary>
        /// Ignore unicode characters and leaves them as-is.
        /// </summary>
        Ignore
    }
}