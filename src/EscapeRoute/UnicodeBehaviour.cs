namespace JackWFinlay.EscapeRoute
{
    /// <summary>
    /// Specifies handling of unicode \uXXXX characters.
    /// </summary>
    public enum UnicodeBehaviour
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