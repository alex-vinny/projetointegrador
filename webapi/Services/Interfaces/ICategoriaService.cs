
using ProjetoIntegrador.Api.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public interface ICategoriaService
    {
        Task<CategoriaItensResponseDto> GetAll();
        Task<CategoriaResponseDto> Get(int id);    
        Task<CategoriaResponseDto> Save(CategoriaRequestDto request);
    }
}