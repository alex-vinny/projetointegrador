using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Api.Data;
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Extensions;
using ProjetoIntegrador.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public class TipoJogoService : Service, ITipoJogoService
    {
        protected readonly BancoContext _context;
        private JogoDicionario _dict;

        public TipoJogoService(BancoContext context)
        {
            _context = context;
            _dict = new JogoDicionario();
        }

        public async Task<List<ResponseDto>> GetAll()
        {
            var jogos = await GetAllJogos();
            if (!jogos.Any())
                return Null("Nenhum jogo cadastrado.").AsList();
            
            return jogos
                .Select(c => c.MakeResponse())
                .ToList();
        }

        public async Task<ResponseDto> GetByCodigo(TiposJogo? tipo)
        {
            if(tipo == null)
                return Null($"Parâmetro de filtro informado está nulo.");

            var jogo = await GetJogoByTipo(tipo.Value);
            if (jogo == null)
                return Null($"Jogo: {tipo.GetDescription()} não localizado.");

            return jogo.MakeResponse();
        }

        public async Task<ResponseDto> GetByCodigoValor(int codigo)
        {
            return await GetByCodigo(_dict[codigo]);
        }

        public async Task<ResponseDto> GetByDescricao(string descricao)
        {
            return await GetByCodigo(_dict[descricao]);
        }

        public async Task<ResponseDto> GetJogo(JogoRequestDto jogoRequestDto)
        {
            if (jogoRequestDto.IsInteger())
            {
                return await GetByCodigoValor(jogoRequestDto.GetValor());
            }
            else if (jogoRequestDto.IsText())
            {
                return await GetByDescricao(jogoRequestDto.ToString());
            }
            else if (jogoRequestDto.IsEnumConvertible())
            {
                return await GetByCodigo(jogoRequestDto.GetEnum());
            }
            else
            {
                var result = await GetAll();
                return result?.FirstOrDefault();
            }
        }

        public async Task<Jogo> Get(JogoRequestDto jogoRequestDto)
        {
            
            if (jogoRequestDto.IsInteger())
            {
                return await GetJogoByTipo(_dict[jogoRequestDto.GetValor()]);
            }
            else if (jogoRequestDto.IsText())
            {
                return await GetJogoByTipo(_dict[jogoRequestDto.ToString()]);
            }
            else if (jogoRequestDto.IsEnumConvertible())
            {
                return await GetJogoByTipo(jogoRequestDto.GetEnum());
            }

            var result = await GetAllJogos();
            return result?.FirstOrDefault();
        }

        private async Task<IList<Jogo>> GetAllJogos()
        {
            return await _context.Jogos.ToListAsync();
        }

        private async Task<Jogo> GetJogoByTipo(TiposJogo? tipo)
        {
            tipo = tipo ?? TiposJogo.InMemory;
            return await _context.Jogos
                .Where(c => c.Codigo == tipo)
                .FirstOrDefaultAsync();
        }
    }
}