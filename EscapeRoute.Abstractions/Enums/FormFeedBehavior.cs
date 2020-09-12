namespace EscapeRoute.Abstractions.Enums
{
    /// <summary>
    /// Specifies handling of form feed \f characters.
    /// </summary>
    public enum FormFeedBehavior
    {
        /// <summary>
        /// Strips form feed \f characters.
        /// </summary>
        Strip = 0,

        /// <summary>
        /// Escapes form feed \f characters.
        /// </summary>
        Escape = 1
    }
}