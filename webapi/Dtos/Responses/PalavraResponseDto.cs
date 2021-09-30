using ProjetoIntegrador.Api.Models;
using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Dtos
{
    public class PalavraResponseDto : ResponseDto
    {
        public string Palavra { get; set; }
        public string Categoria { get; set; }
        public string DicaPalavra { get; set; }
        public int? SerieEscolar { get; set; }
        public int? CategoriaId { get; set; }

        public Palavra ToModel()
        {
            return new Palavra
            {
                Categoria = new Categoria
                {
                    ID = CategoriaId.Value,
                    Descricao = Categoria
                },
                Dica = DicaPalavra,
                SerieEscolar = SerieEscolar,
                Valor = Palavra
            };
        }
    }
}