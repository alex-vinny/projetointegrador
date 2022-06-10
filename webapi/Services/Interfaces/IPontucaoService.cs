using ProjetoIntegrador.Api.Dtos;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public interface IPontucaoService
    {
        Task<ResponseDto> Update(int id, PontuacaoRequestDto request);
        Task<ResponseDto> Save(PontuacaoCreationRequestDto request);
    }
}