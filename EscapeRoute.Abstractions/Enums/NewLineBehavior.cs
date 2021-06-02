namespace EscapeRoute.Abstractions.Enums
{
    public enum NewLineBehavior
    {
        /// <summary>
        /// Removes all new line characters (\n).
        /// </summary>
        Strip,

        /// <summary>
        /// Escapes new line characters (\n) with a space character (' ').
        /// </summary>
        Space,
        
        /// <summary>
        /// Escapes new line characters (\n) with the literal characters '\' and 'n'.
        /// </summary>
        Escape,
        
        /// <summary>
        /// Ignore new line characters (\n). Leave them as is.
        /// </summary>
        Ignore
    }
}