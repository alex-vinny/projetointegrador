using ProjetoIntegrador.Api.Dtos;
using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Models
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Descricao { get; set; }

        public CategoriaDto MakeDto() => new CategoriaDto(MakeResponse());

        public CategoriaResponseDto MakeResponse()
        {
            return new CategoriaResponseDto
            {
                Id = ID,
                Categoria = Descricao
            };
        }
    }
}