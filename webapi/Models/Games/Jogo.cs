using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Extensions;
using System.ComponentModel;

namespace ProjetoIntegrador.Api.Models
{
    public enum TiposJogo
    {
        [Description("REFLECTERE")]
        Reflectere = 1,
        [Description("INMEMORY")]
        InMemory = 2
    }

    public class Jogo
    {
        public int ID { get; set; }
        public TiposJogo Codigo { get; set; }
        public string Descricao { get; set; }

        public ResponseDto MakeResponse()
        {
            return new ResponseDto
            {
                {"id", ID},
                {"codigo", Codigo.GetDescription() },
                {"descricao", Descricao},
            };
        }
    }
}