using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Config
{
    public interface IConnectionStringParser
    {
        string GetPort();
        string GetHost();
        string GetUser();
        string GetPass();
        string GetDatabase();
        string GetConnectionString();
    }
}