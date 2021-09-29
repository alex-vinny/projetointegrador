using System;

namespace ProjetoIntegrador.Api.Config
{
    public class EnvironmentConnectionString : IConnectionString
    {
        public IStringParser GetParser()
        {
            string s = Environment.GetEnvironmentVariable(Constants.DATABASE_ENV);
            return new PgStringParser(s);
        }
    }
}