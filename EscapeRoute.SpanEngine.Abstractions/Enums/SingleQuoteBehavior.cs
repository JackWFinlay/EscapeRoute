namespace EscapeRoute.SpanEngine.Abstractions.Enums
{
    public enum SingleQuoteBehavior
    {
        /// <summary>
        /// Replace double quotes '...' with escaped single quotes \'...\'.
        /// </summary>
        Single = 0,
        /// <summary>
        /// Replace double quotes '...' with escaped double quotes \"...\".
        /// </summary>
        Double = 1
    }
}