using ProjetoIntegrador.Api.Data;
using ProjetoIntegrador.Api.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public class TipoJogoService : Service, ITipoJogoService
    {
        protected readonly BancoContext _context;

        public TipoJogoService(BancoContext context)
        {
            _context = context;
        }

        public async Task<List<ResponseDto>> GetAll()
        {
            var jogo1 = new ResponseDto();
            jogo1.Add("id", 1);
            jogo1.Add("codigo", "REFLECTERE");
            jogo1.Add("descricao", "Reflectere: Jogo de Palavras Cruzadas");
            var jogo2 = new ResponseDto();
            jogo2.Add("id", 2);
            jogo2.Add("codigo", "INMEMORY");
            jogo2.Add("descricao", "In Memory: Jogo da Memória");

            return await Task
                 .FromResult(new List<ResponseDto> { jogo1, jogo2 });
        }
    }
}