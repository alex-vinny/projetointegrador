namespace ProjetoIntegrador.Api.Config
{
    public interface IStringParser
    {
        string GetPort();
        string GetHost();
        string GetUser();
        string GetPass();
        string GetDatabase();
        string GetConnectionString();
    }
}