using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Dtos.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public interface IImagemService
    {
        Task<List<ResponseDto>> GetByCategoria(string categoria, int quantidade);
        Task<List<ResponseDto>> GetImagensAsResponse(int take, int? categoria = null);
        Task<List<ImagemDto>> GetImagens(int take, int? categoria = null);
    }
}