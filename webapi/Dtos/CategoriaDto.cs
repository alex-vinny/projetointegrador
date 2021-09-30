using ProjetoIntegrador.Api.Models;

namespace ProjetoIntegrador.Api.Dtos
{
    public class CategoriaDto : ModelDto
    {
        public CategoriaDto(CategoriaRequestDto request)
        {
            Categoria = request.Categoria;
        }

        public CategoriaDto(CategoriaResponseDto response)
        {
            Categoria = response.Categoria;
            Id = response.Id;
        }

        public string Categoria { get; set; }

        public Categoria ToModel()
        {
            return new Categoria
            {
                Descricao = Categoria
            };
        }
    }
}