using ProjetoIntegrador.Api.Dtos;
using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Models
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Descricao { get; set; }

        public ResponseDto MakeResponse()
        {
            return new ResponseDto
            {
                { "id", ID },
                { "descricao", Descricao }
            };
        }
    }
}