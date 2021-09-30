namespace ProjetoIntegrador.Api.Config
{
    public class PgStringParser : IStringParser
    {
        string connection;

        public PgStringParser(string connection)
        {
            this.connection = connection;
        }

        public bool Fail => string.IsNullOrEmpty(connection);

        private string GetLeft()
        {
            return connection.Split("@")[0].Replace("postgres://", "");
        }

        public string GetUser()
        {
            return GetLeft().Split(":")[0];
        }

        public string GetPass()
        {
            return GetLeft().Split(":")[1];
        }

        private string GetRight()
        {
            return connection.Split("@")[1];
        }

        public string GetHost()
        {
            return GetRight().Split(":")[0];
        }

        private string GetPortAndDatabase()
        {
            return GetRight().Split(":")[1];
        }

        public string GetPort()
        {
            return GetPortAndDatabase().Split("/")[0];
        }

        public string GetDatabase()
        {
            return GetPortAndDatabase().Split("/")[1];
        }

        public string GetConnectionString()
        {
            return $"User ID={GetUser()};Password={GetPass()};Server={GetHost()};Port={GetPort()};Database={GetDatabase()};SSL Mode=Require;Trust Server Certificate=true;Integrated Security=true;Pooling=true;";
        }
    }
}