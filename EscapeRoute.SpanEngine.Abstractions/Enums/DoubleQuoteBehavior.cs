namespace EscapeRoute.SpanEngine.Abstractions.Enums
{
    public enum DoubleQuoteBehavior
    {
        /// <summary>
        /// Replace double quotes "..." with escaped double quotes \"...\".
        /// </summary>
        Double = 0,
        /// <summary>
        /// Replace double quotes "..." with escaped single quotes \'...\'.
        /// </summary>
        Single = 1
    }
}