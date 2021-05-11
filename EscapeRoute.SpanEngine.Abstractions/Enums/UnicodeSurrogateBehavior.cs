namespace EscapeRoute.SpanEngine.Abstractions.Enums
{
    public enum UnicodeSurrogateBehavior
    {
        
        /// <summary>
        /// Remove all instances of surrogate pair Unicode characters.
        /// </summary>
        Strip,
        
        /// <summary>
        /// Escape all instances of surrogate pair Unicode characters in the form "\uXXXX\uXXXX".
        /// </summary>
        Escape,
        
        /// <summary>
        /// Leave Unicode surrogate pair characters as-is.
        /// </summary>
        Ignore
    }
}