namespace ProjetoIntegrador.Api.Config
{
    public enum DatabaseType
    {
        EnvironmentVariable,
        AppSettings
    }

    public interface IConnectionString
    {
        DatabaseType TypeOf { get; }
        IStringParser GetParser();
    }
}