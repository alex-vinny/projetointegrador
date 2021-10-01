
using ProjetoIntegrador.Api.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public interface IUsuarioService
    {
        Task<ResponseDto> GetAll();
        Task<ResponseDto> GetByEmail(string email);
        Task<ResponseDto> Get(int id);
        Task<ResponseDto> Update(string email, UsuarioRequestDto request);
        Task<ResponseDto> Save(UsuarioDto request);
        Task<ResponseDto> Delete(int id);
        Task<ResponseDto> DeleteByEmail(string email);
    }
}