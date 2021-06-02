namespace EscapeRoute.Abstractions.Enums
{
    public enum CarriageReturnBehavior
    {
        /// <summary>
        /// Strip carriage return \r characters.
        /// </summary>
        Strip,
        
        /// <summary>
        /// Escape carriage return \r characters. Replace them with the literal characters '\' and 'r'.
        /// </summary>
        Escape,
        
        /// <summary>
        /// Ignores carriage return \r characters. Leave them as-is.
        /// </summary>
        Ignore
    }
}