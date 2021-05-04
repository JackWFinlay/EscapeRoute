namespace EscapeRoute.SpanEngine.Abstractions.Enums
{
    public enum UnicodeSurrogateBehavior
    {
        /// <summary>
        /// Leave Unicode surrogate pair characters as-is.
        /// </summary>
        Ignore = 0,
        /// <summary>
        /// Remove all instances of surrogate pair Unicode characters.
        /// </summary>
        Strip = 1,
        /// <summary>
        /// Escape all instances of surrogate pair Unicode characters in the form "\uXXXX\uXXXX".
        /// </summary>
        Escape = 2
    }
}