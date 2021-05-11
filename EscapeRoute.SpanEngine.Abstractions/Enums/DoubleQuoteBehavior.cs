namespace EscapeRoute.SpanEngine.Abstractions.Enums
{
    public enum DoubleQuoteBehavior
    {
        /// <summary>
        /// Replace double quotes "..." with escaped double quotes \"...\".
        /// </summary>
        Double,
        
        /// <summary>
        /// Replace double quotes "..." with escaped single quotes \'...\'.
        /// </summary>
        Single,

        /// <summary>
        /// Ignore double quote characters "...". Leave them as-is.
        /// </summary>
        Ignore
    }
}