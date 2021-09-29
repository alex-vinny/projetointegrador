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

        public IStringParser GetParser()
        {
            string s = configuration.GetConnectionString(Constants.DATABASE_SETT);
            return new SimpleStringParser(s);
        }
    }
}