namespace JackWFinlay.EscapeRoute
{
    /// <summary>
    /// Specifies handling of carriage return \r characters.
    /// </summary>
    public enum CarriageReturnBehaviour
    {
        /// <summary>
        /// Strips carriage return \r characters.
        /// </summary>
        Strip = 0,

        /// <summary>
        /// Escapes carriage return \r characters.
        /// </summary>
        Escape = 1
    }
}