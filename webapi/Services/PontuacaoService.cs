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
        private readonly ITipoJogoService _jogoService;
        private readonly IUsuarioService _usuarioService;

        public PontuacaoService(BancoContext context,
            ITipoJogoService jogoService,
            IUsuarioService usuarioService)
        {
            _context = context;
            _jogoService = jogoService;
            _usuarioService = usuarioService;
        }

        public async Task<ResponseDto> Save(PontuacaoCreationRequestDto request)
        {
            Usuario usuario = null;
            if (request.Usuario != null)
                usuario = await _usuarioService.GetUsuario(request.Usuario.Value);
            else
                usuario = await _usuarioService.GetUsuarioByEmail(request.Email);

            object parameter = null;
            if (request.TipoJogoId.HasValue)
                parameter = request.TipoJogoId.Value;
            else
                parameter = request.TipoJogo;

            Jogo jogo = await _jogoService.Get(new JogoRequestDto(parameter));

            return await Save(usuario, jogo, request);
        }
        
        public async Task<ResponseDto> Update(int id, PontuacaoRequestDto request)
        {
            try
            {
                var model = await GetPontuacao(id);
                if (model == null)
                    return Error($"Pontuação com o identificador: {id} não localizado.");
                
                model.Pontos = request.QtdPontos;
                model.Itens = request.QtdItems;
                model.Erros = request.QtdErros;

                await UpdatePontuacao(model.ID, model);

                return model.MakeResponse();
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        private async Task<Pontuacao> GetPontuacao(int id)
        {
            return await _context
                .Pontuacoes
                .Include(c => c.Jogo)
                .Include(c => c.Autor)
                .FirstOrDefaultAsync(c => c.ID.Equals(id));
        }

        private async Task UpdatePontuacao(int id, Pontuacao model)
        {
            if (ExistsPontuacao(id))
            {
                var entity = await _context.Pontuacoes.FindAsync(id);
                _context.Entry(entity).State = EntityState.Modified;
                _context.Pontuacoes.Update(model);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<ResponseDto> Save(Usuario usuario, Jogo jogo, PontuacaoCreationRequestDto request)
        {
            try
            {
                if (usuario == null)
                    return Null($"Não foi possível salvar a pontuação. Usuário não cadastrado.");

                if (jogo == null)
                    return Null($"Não foi possível salvar a pontuação. Tipo de jogo não cadastrado.");

                var model = new Pontuacao();
                model.Autor = usuario;
                model.Jogo = jogo;
                model.DataJogo = request.DataJogo.HasValue ? request.DataJogo.Value : DateTime.UtcNow;
                model.Pontos = request.QtdPontos;
                model.Itens = request.QtdItems;
                model.Erros = request.QtdErros;

                await SavePontuacao(model);

                return model.MakeResponse();
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        private async Task<Pontuacao> SavePontuacao(Pontuacao model)
        {
            _context.Pontuacoes.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        private bool ExistsPontuacao(int id)
        {
            return _context.Pontuacoes.Any(e => e.ID == id);
        }
    }
}