namespace EscapeRoute.Abstractions.Enums
{
    /// <summary>
    /// Specifies handling of carriage return \r characters.
    /// </summary>
    public enum CarriageReturnBehavior
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