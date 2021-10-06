using ProjetoIntegrador.Api.Models;
using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Dtos
{
    public class SessaoDto
    {
        public DateTime? DataInicio { get; set; }
        public int? Acertos { get; internal set; }
        public DateTime? DataFim { get; internal set; }
    }
}