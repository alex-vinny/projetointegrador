using ProjetoIntegrador.Api.Dto;
using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Models
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Descricao { get; set; }

        public CategoriaDto ToDto()
        {
            return new CategoriaDto
            {

            };
        }
    }
}