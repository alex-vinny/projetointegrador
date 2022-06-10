
using ProjetoIntegrador.Api.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public interface ITipoJogoService
    {
        Task<List<ResponseDto>> GetAll();
    }
}