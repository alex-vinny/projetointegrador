using ProjetoIntegrador.Api.Config;
using System;

namespace ProjetoIntegrador.Api.Models.Exceptions
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string configKey)
            : base(string.Format(Constants.CONFIG_DEFAULT_MSG, configKey))
        {
        }
    }
}
