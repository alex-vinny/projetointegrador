using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public interface ICategoriaService
    {
        Task<ResponseDto> GetAll();
        Task<Categoria[]> GetAllCategoria();
        Task<ResponseDto> Get(int id);    
        Task<ResponseDto> Save(CategoriaDto request);
    }
}