using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Models
{
    public class Palavra
    {
        public int ID { get; set; }
        public string Valor { get; set; }
        public string Dica { get; set; }
        public Categoria Categoria { get; set; }
    }
}