
using ProjetoIntegrador.Api.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioResponseDto>> GetAll();
        Task<UsuarioResponseDto> GetByEmail(string email);
        Task<UsuarioResponseDto> Get(int id);
        Task<ResponseDto> Update(int id, UsuarioDto request);
        Task<UsuarioResponseDto> Save(UsuarioDto request);
        Task<ResponseDto> Delete(int id);
    }
}