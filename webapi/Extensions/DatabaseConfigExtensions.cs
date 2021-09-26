using System;
using Microsoft.Extensions.DependencyInjection;
using ProjetoIntegrador.Api.Config;

namespace ProjetoIntegrador.Api.Extensions
{
    public static class DatabaseConfigExtensions
    {
        public static string GetConnectionString(this IServiceCollection s)
        {
            string v = Environment.GetEnvironmentVariable("DATABASE_URL");

            return GetParser(v).GetConnectionString();
        }

        private static IConnectionStringParser GetParser(string conn)
        {
            return new PostgreStringParser(conn);
        }
    }
}
