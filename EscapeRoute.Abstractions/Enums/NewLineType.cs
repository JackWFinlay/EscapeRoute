namespace EscapeRoute.Abstractions.Enums
{
    public enum NewLineType
    {
        /// <summary>
        /// Do not join lines with a separator.
        /// </summary>
        Strip = 0,
        /// <summary>
        /// Separates new lines with a space character (' ').
        /// </summary>
        Space = 1,
        /// <summary>
        /// Separates new lines with the newline character ('\n').
        /// </summary>
        Escape = 2,
        /// <summary>
        /// Separates new lines with the carriage return and newline character ('\r\n').
        /// </summary>
        Windows = 3,
        /// <summary>
        /// Separates new lines with the newline character ('\n').
        /// </summary>
        Unix = 4
    }
}