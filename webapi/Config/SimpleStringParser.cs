namespace ProjetoIntegrador.Api.Config
{
    public class SimpleStringParser : IStringParser
    {
        string connection;

        public SimpleStringParser(string connection)
        {
            this.connection = connection;
        }

        public bool Fail => string.IsNullOrEmpty(connection);
        
        public string GetUser()
        {
            return connection;
        }

        public string GetPass()
        {
            return connection;
        }

        public string GetHost()
        {
            return connection;
        }
        
        public string GetPort()
        {
            return connection;
        }

        public string GetDatabase()
        {
            return connection;
        }

        public string GetConnectionString()
        {
            return connection;
        }
    }
}