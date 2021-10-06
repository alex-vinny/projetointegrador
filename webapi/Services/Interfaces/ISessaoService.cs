using ProjetoIntegrador.Api.Dtos;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public interface ISessaoService
    {
        Task<ResponseDto> Update(string email, SessaoRequestDto request);
        Task<ResponseDto> Save(string email, SessaoDto request);
    }
}