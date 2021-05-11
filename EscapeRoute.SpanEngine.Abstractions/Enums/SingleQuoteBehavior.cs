namespace EscapeRoute.SpanEngine.Abstractions.Enums
{
    public enum SingleQuoteBehavior
    {
        /// <summary>
        /// Replace double quotes '...' with escaped single quotes \'...\'.
        /// </summary>
        Single,
        
        /// <summary>
        /// Replace double quotes '...' with escaped double quotes \"...\".
        /// </summary>
        Double,
        
        /// <summary>
        /// Ignore single quotes '...'. Leave them as-is.
        /// </summary>
        Ignore
    }
}