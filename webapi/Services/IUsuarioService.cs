
using ProjetoIntegrador.Api.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetAll();
        Task<UsuarioDto> Get(int id);
        Task<UsuarioDto> Update(int id, UsuarioDto usuario);
        Task<UsuarioDto> Save(UsuarioDto usuario);
        Task<UsuarioDto> Delete(int id);
    }
}