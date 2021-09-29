namespace ProjetoIntegrador.Api.Config
{
    public interface IConnectionString
    {
        IStringParser GetParser();
    }
}