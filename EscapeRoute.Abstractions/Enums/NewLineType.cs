namespace EscapeRoute.Abstractions.Enums
{
    public enum NewLineType
    {
        /// <summary>
        /// Do not join lines with a separator.
        /// </summary>
        None = 0,
        /// <summary>
        /// Separates new lines with a space character (' ').
        /// </summary>
        Space = 1,
        /// <summary>
        /// Separates new lines with the newline character ('\n').
        /// </summary>
        Unix = 2,
        /// <summary>
        /// Separates new lines with the carriage return and newline character combination ('\r\n').
        /// </summary>
        Windows = 3
    }
}