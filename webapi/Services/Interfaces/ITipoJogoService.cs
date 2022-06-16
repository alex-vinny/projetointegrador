
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public interface ITipoJogoService
    {
        Task<List<ResponseDto>> GetAll();
        Task<ResponseDto> GetByCodigo(TiposJogo? tipo);
        Task<ResponseDto> GetByCodigoValor(int codigo);
        Task<ResponseDto> GetByDescricao(string descricao);
        Task<ResponseDto> GetJogo(JogoRequestDto jogoRequestDto);
        Task<Jogo> Get(JogoRequestDto jogoRequestDto);
    }
}