namespace JackWFinlay.EscapeRoute
{
    /// <summary>
    /// Specifies handling of new line \n characters.
    /// </summary>
    public enum NewLineBehavior
    {
        /// <summary>
        /// Strips new line \n characters.
        /// </summary>
        Strip = 0,

        /// <summary>
        /// Escapes new line \n characters.
        /// </summary>
        Escape = 1
    }
}