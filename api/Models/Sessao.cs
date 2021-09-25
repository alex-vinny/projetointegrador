using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Models
{
    public class Sessao
    {
        public int ID { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public Cruzada Cruzada { get; set; }
        public int? Acertos { get; set; }
        public Usuario Usuario {get; set; }
    }
}