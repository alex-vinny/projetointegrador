using System;

namespace ProjetoIntegrador.Api.Config
{
    public class EnvironmentConnectionString : IConnectionString
    {
        public DatabaseType TypeOf => DatabaseType.EnvironmentVariable;

        public IStringParser GetParser()
        {
            string s = Environment.GetEnvironmentVariable(Constants.DATABASE_ENV);
            return new PgStringParser(s);
        }
    }
}