namespace EscapeRoute.Abstractions.Interfaces
{
    public interface ITokenReplacementConfigurationBuilder
    {
        ITokenReplacementConfigurationBuilder AddMapping(string token,
            string substitution);
        
        
    }
}