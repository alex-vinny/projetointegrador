using ProjetoIntegrador.Api.Extensions;
using ProjetoIntegrador.Api.Models;

namespace ProjetoIntegrador.Api.Dtos
{
    public class CategoriaDto : IModelDto
    {
        public CategoriaDto(string categoria)
        {
            Categoria = categoria;
        }

        public string Categoria { get; set; }
        public int Id { get; set; }

        public Categoria ToModel()
        {
            return new Categoria
            {
                Descricao = Categoria,
                DescricaoSemAcento = Categoria.RemoverAcentos(true)
            };
        }
    }
}