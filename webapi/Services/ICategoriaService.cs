
using ProjetoIntegrador.Api.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDto>> GetAll();
        Task<CategoriaDto> Get(int id);
        Task<CategoriaDto> Update(int id, CategoriaDto usuario);
        Task<CategoriaDto> Save(CategoriaDto usuario);
        Task<CategoriaDto> Delete(int id);
    }
}