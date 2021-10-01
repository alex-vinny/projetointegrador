using ProjetoIntegrador.Api.Models;
using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Dtos
{
    public class PalavraRequestDto
    {
        public string Palavra { get; set; }
        public string Categoria { get; set; }
        public string DicaPalavra { get; set; }
        public int? SerieEscolar { get; set; }
    }
}