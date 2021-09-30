using Microsoft.Extensions.Configuration;

namespace ProjetoIntegrador.Api.Config
{
    public class SettingsConnectionString : IConnectionString
    {
        private readonly IConfiguration configuration;

        public SettingsConnectionString(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DatabaseType TypeOf => DatabaseType.AppSettings;

        public IStringParser GetParser()
        {
            string s = configuration.GetConnectionString(Constants.DATABASE_SETT);
            return new SimpleStringParser(s);
        }
    }
}