using ProjetoIntegrador.Api.Dtos;
using System;

namespace ProjetoIntegrador.Api.Models
{
    public class Pontuacao
    {
        public int ID { get; set; }
        public int? Pontos { get; set; }
        public int? Erros { get; set; }
        public int? Itens { get; set; }
        public DateTime? DataJogo { get; set; }
        public Jogo Jogo { get; set; }        
        public Usuario Autor { get; set; }

        public ResponseDto MakeResponse()
        {
            var response = new ResponseDto
            {
                { "id", ID },                
                { "tipoJogo", Jogo.MakeResponse() },
                { "data", DataJogo?.ToString("d") },
                { "erros", Erros },
                { "pontos", Pontos },
                { "itens", Itens },
            };

            if (Autor != null)
                response.Add("usuario", Autor.MakeResponse());

            return response;
        }
    }
}