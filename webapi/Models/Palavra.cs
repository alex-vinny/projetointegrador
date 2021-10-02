using ProjetoIntegrador.Api.Dtos;
using System;
using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Models
{
    public class Palavra
    {
        public int ID { get; set; }
        public string Valor { get; set; }
        public string ValorSemAcento { get; set; }
        public string Dica { get; set; }
        public string DicaSemAcento { get; set; }
        public int? SerieEscolar { get; set; }
        public Categoria Categoria { get; set; }

        public ResponseDto MakeResponse()
        {
            return new ResponseDto
            {
                { "id", ID },
                { "palavra", Valor },
                { "palavraSemAcento", ValorSemAcento },
                { "dica", Dica },
                { "dicaSemAcento", DicaSemAcento },
                { "serieEscolar", SerieEscolar },
                { "categoria", Categoria.Descricao },
            };
        }
    }
}