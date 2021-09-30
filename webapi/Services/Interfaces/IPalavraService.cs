
using ProjetoIntegrador.Api.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public interface IPalavraService
    {
        Task<IEnumerable<PalavraResponseDto>> GetAll(PalavraDto request);
        Task<PalavraResponseDto> Get(int id);
        Task<PalavraResponseDto> GetByValor(string palavra);
        Task<ResponseDto> Update(int id, PalavraDto request);
        Task<ResponseDto> UpdateCategoria(string palavra, string categoria);
        Task<PalavraResponseDto> Save(PalavraDto request);
        Task<ResponseDto> Delete(int id);
        Task<ResponseDto> DeleteByValor(string palavra);
    }
}