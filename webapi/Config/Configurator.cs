using Microsoft.Extensions.Configuration;
using ProjetoIntegrador.Api.Models.Exceptions;
using System;

namespace ProjetoIntegrador.Api.Config
{
    public class Configurator
    {
        private readonly IConfiguration configuration;
        
        public Configurator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Get(string key)
        {
            string value = Environment.GetEnvironmentVariable(key);

            if (string.IsNullOrEmpty(value))
                value = configuration[key];

            if (string.IsNullOrEmpty(value))
                throw new ConfigurationException(key);

            return value;
        }
    }
}
