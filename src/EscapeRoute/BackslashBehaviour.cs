namespace JackWFinlay.EscapeRoute
{
    /// <summary>
    /// Specifies handling of backslash \\ characters.
    /// </summary>
    public enum BackslashBehaviour
    {
        /// <summary>
        /// Strips backslash \\ characters.
        /// </summary>
        Strip = 0,

        /// <summary>
        /// Escapes backslash \\ characters.
        /// </summary>
        Escape = 1
    }
}