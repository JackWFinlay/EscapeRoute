namespace EscapeRoute.SpanEngine.Abstractions.Enums
{
    /// <summary>
    /// Specifies handling of form feed \f characters.
    /// </summary>
    public enum FormFeedBehavior
    {
        /// <summary>
        /// Strips form feed \f characters.
        /// </summary>
        Strip,

        /// <summary>
        /// Escapes form feed \f characters.
        /// </summary>
        Escape,
        
        /// <summary>
        /// Ignore form feed \f characters.
        /// </summary>
        Ignore
    }
}