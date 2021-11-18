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
    public class SessaoService : Service, ISessaoService
    {
        protected readonly BancoContext _context;

        public SessaoService(BancoContext context)
        {
            _context = context;
        }

        private async Task<Usuario> GetUsuarioByEmail(string email)
        {
            return await _context.Usuarios
                            .Where(c => c.Email.Equals(email))
                            .FirstOrDefaultAsync();
        }

        public async Task<ResponseDto> Update(string email, SessaoRequestDto request)
        {
            try
            {
                var usuario = await GetUsuarioByEmail(email);
                if (usuario == null)
                    return Null($"N�o foi poss�vel iniciar o jogo. Usu�rio com o e-mail: {email} n�o cadastrado.");

                var model = await GetSessao(request.Id);
                if (model == null)
                {
                    model = new Sessao
                    {
                        Usuario = usuario,
                        Inicio = DateTime.UtcNow,
                        Cruzada = new Cruzada()
                    };
                }

                if (request.AdicionarAcertos != null)
                    model.Acertos = model.Acertos + request.AdicionarAcertos;
                else if (request.TotalAcertos != null)
                    model.Acertos = request.TotalAcertos;

                model.Fim = request.DataFim;

                await UpdateSessao(model.ID, model);

                return new ResponseDto
                {
                    { "usuario", model.MakeResponse() }
                };
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        private async Task<Sessao> GetSessao(int id)
        {
            return await _context.Sessoes.FindAsync(id);
        }

        private async Task UpdateSessao(int id, Sessao model)
        {
            if (ExistsSessao(id))
            {
                var entity = await _context.Sessoes.FindAsync(id);
                _context.Entry(entity).State = EntityState.Modified;
                _context.Sessoes.Update(model);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<ResponseDto> Save(string email, SessaoDto request)
        {
            try
            {
                var usuario = await GetUsuarioByEmail(email);
                if (usuario == null)
                    return Null($"N�o foi poss�vel iniciar o jogo. Usu�rio com o e-mail: {email} n�o cadastrado.");

                var model = new Sessao();
                model.Usuario = usuario;
                model.Inicio = request.DataInicio.HasValue ? request.DataInicio.Value : DateTime.UtcNow;
                model.Cruzada = new Cruzada();
                model.Acertos = request.Acertos;
                model.Fim = request.DataFim;

                await SaveSessao(model);

                return new ResponseDto
                {
                    { "sessao", model.MakeResponse() }
                };
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        private async Task<Sessao> SaveSessao(Sessao model)
        {
            _context.Sessoes.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        private bool ExistsSessao(int id)
        {
            return _context.Usuarios.Any(e => e.ID == id);
        }
    }
}