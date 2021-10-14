using Microsoft.Extensions.Configuration;
using System;

namespace ProjetoIntegrador.Api.Config
{
    public class ConnectionStringStrategy : IConnectionString
    {
        IStringParser parser;

        public ConnectionStringStrategy(IConfiguration configuration)
        {
            IConnectionString env = new EnvironmentConnectionString();
            IConnectionString sett = new SettingsConnectionString(configuration);

            parser = env.GetParser();
            TypeOf = env.TypeOf;
            if (parser.Fail)
            {
                parser = sett.GetParser();
                TypeOf = sett.TypeOf;
            }
        }

        public DatabaseType TypeOf { get; private set; }

        public IStringParser GetParser()
        {
            return parser;
        }
    }
}