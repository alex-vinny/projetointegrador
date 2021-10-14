using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjetoIntegrador.Api.Config;
using ProjetoIntegrador.Api.Data;

namespace ProjetoIntegrador.Api.Extensions
{
    public static class DatabaseConfigExtensions
    {
        public static void ConfigureDatabase(this IConnectionString connection, IServiceCollection services)
        {
            if (connection.TypeOf == DatabaseType.EnvironmentVariable)
            {
                services.AddDbContext<BancoContext>(options =>
                                options.UseNpgsql(connection.GetConnectionString()));
            }
            else if(connection.TypeOf == DatabaseType.AppSettings)
            {
                //services.AddDatabaseDeveloperPageExceptionFilter();
                services.AddDbContext<BancoContext>(options =>
                        options.UseSqlite(connection.GetConnectionString()));
            }
        }
        public static string GetConnectionString(this IConnectionString service)
        {
            return service.GetParser().GetConnectionString();
        }
    }
}
