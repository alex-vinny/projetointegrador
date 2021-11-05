
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public interface IPalavraService
    {
        Task<Palavra[]> GetAllPalavras();

        
        Task<Palavra[]> GetPalavrasByCategoriaQtd(int categoria, int qtd);


        Task<ResponseDto> GetAll(PalavraDto request);
        Task<ResponseDto> Get(int id);
        Task<ResponseDto> GetByValor(string palavra);
        Task<ResponseDto> Update(PalavraRequestDto request);
        Task<ResponseDto> UpdateCategoria(string palavra, string categoria);
        Task<ResponseDto> Save(PalavraRequestDto request);
        Task<ResponseDto> Delete(int id);
        Task<ResponseDto> DeleteByValor(string palavra);
    }
}