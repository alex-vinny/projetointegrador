using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Api.Data;
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public class PontuacaoService : Service, IPontucaoService
    {
        protected readonly BancoContext _context;

        public PontuacaoService(BancoContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto> Save(PontuacaoCreationRequestDto request)
        {
            return await Task.FromResult(GetMockResponse());
        }

        public async Task<ResponseDto> Update(int id, PontuacaoRequestDto request)
        {
            return await Task.FromResult(GetMockResponse(id));
        }

        private ResponseDto GetMockResponse(int? id = null)
        {
            id = id ?? new Random().Next(1, 100);
            var jogo = new ResponseDto();
            jogo.Add("id", id);
            jogo.Add("codigo", "REFLECTERE");
            jogo.Add("descricao", "Reflectere: Jogo de Palavras Cruzadas");

            var usuario = new ResponseDto();
            usuario.Add("id", new Random().Next(1, 100));
            usuario.Add("email", "email@email.com");
            usuario.Add("nome", "Jogador #1");

            var dto = new ResponseDto();
            dto.Add("id", new Random().Next(1, 1000));
            dto.Add("usuario", usuario);
            dto.Add("tipoJogo", jogo);
            dto.Add("data", DateTime.Now);
            dto.Add("pontos", 15);
            dto.Add("erros", 2);
            dto.Add("itens", 20);

            return dto;
        }
    }
}