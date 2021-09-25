using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Models
{
    public class Professor : Usuario
    {
        public Professor() : base(Perfis.Professor) { }
        public string Disciplina { get; set; }
    }
}