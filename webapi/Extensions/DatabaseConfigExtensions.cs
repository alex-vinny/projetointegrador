using System;
using Microsoft.Extensions.DependencyInjection;
using ProjetoIntegrador.Api.Config;

namespace ProjetoIntegrador.Api.Extensions
{
    public static class DatabaseConfigExtensions
    {
        public static string GetConnectionString(this IConnectionString service)
        {
            return service.GetParser().GetConnectionString();
        }
    }
}
