using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Models
{
    public class Aluno : Usuario
    {
        public Aluno() : base(Perfis.Aluno) { }
        
        public int? Idade { get; set; }
        public Sexos? Sexo { get; set; }
        public int? SerieEscolar { get; set; }
    }
}