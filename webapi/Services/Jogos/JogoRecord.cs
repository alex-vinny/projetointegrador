using ProjetoIntegrador.Api.Extensions;
using ProjetoIntegrador.Api.Models;

namespace ProjetoIntegrador.Api.Services
{
    public class JogoRecord
    {
        public JogoRecord(TiposJogo tipo)
        {
            Tipo = tipo;
        }

        public TiposJogo Tipo { get; }
        public int Valor => (int)Tipo;
        public string Descricao => Tipo.GetDescription();
    }
}