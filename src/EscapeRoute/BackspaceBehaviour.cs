namespace JackWFinlay.EscapeRoute
{
    /// <summary>
    /// Specifies handling of backspace \b characters.
    /// </summary>
    public enum BackspaceBehaviour
    {
        /// <summary>
        /// Strips backspace \b characters.
        /// </summary>
        Strip = 0,

        /// <summary>
        /// Escapes backspace \b characters.
        /// </summary>
        Escape = 1
    }
}