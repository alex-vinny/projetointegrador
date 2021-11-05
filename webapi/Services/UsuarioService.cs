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
    public class UsuarioService : Service, IUsuarioService
    {
        protected readonly BancoContext _context;

        public UsuarioService(BancoContext context)
        {
            _context = context;
        }

        public async Task<List<ResponseDto>> GetAll()
        {
            return await GetAll(RequestDto.DefaultPagination);
        }

        public async Task<List<ResponseDto>> GetAll(RequestDto dto)
        {
            try
            {
                var query = _context.Usuarios
                    .OrderBy(c => c.ID)
                    .Skip(dto.Skip.Value)
                    .Take(dto.Take.Value)
                    .Select(c => c.MakeResponse());

                var usuarios = await query.ToListAsync();

                if (!usuarios.Any())
                    Null("Nenhum usu�rio cadastrado.");

                return usuarios;
            }
            catch (Exception ex)
            {
                return new[]{Exception(ex)}.ToList();
            }
        }
        
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                var model = await GetUsuario(id);
                if (model == null)
                    return Null($"Usu�rio com o ID: {id} n�o localizado.");

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

        private async Task<Usuario> GetUsuario(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<ResponseDto> GetByEmailSenha(string email, string senha)
        {
            try
            {
                var model = await GetUsuarioByEmail(email, senha);
                if (model == null)
                    return Null($"Usuário ou senha incorreto");

                return model.MakeResponse();                
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        private async Task<Usuario> GetUsuarioByEmail(string email)
        {
            return await _context.Usuarios
                            .Where(c => c.Email.Equals(email))
                            .FirstOrDefaultAsync();
        }

        private async Task<Usuario> GetUsuarioByEmail(string email, string senha)
        {
            return await _context.Usuarios
                            .Where(c => c.Email.Equals(email) && c.Senha.Equals(senha))
                            .FirstOrDefaultAsync();
        }

        public async Task<ResponseDto> Update(string email, UsuarioRequestDto request)
        {
            try
            {
                var model = await GetUsuarioByEmail(email);
                if (model == null)
                    return Null($"Usu�rio com o e-mail: {email} n�o cadastrado.");
                
                model.Nome = string.IsNullOrEmpty(request.Nome) ? model.Nome : request.Nome;
                model.DicaSecreta = string.IsNullOrEmpty(request.DicaSecreta) ? model.DicaSecreta : request.DicaSecreta;
                model.PalavraSecreta = string.IsNullOrEmpty(request.PalavraSecreta) ? model.PalavraSecreta : request.PalavraSecreta;
                model.Senha = string.IsNullOrEmpty(request.Senha) ? model.Senha : request.Senha;

                await Update(model.ID, model);

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

        private async Task Update(int id, Usuario model)
        {
            if (Exists(id))
            {
                var entity = await _context.Usuarios.FindAsync(id);
                _context.Entry(entity).State = EntityState.Modified;
                _context.Usuarios.Update(model);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<ResponseDto> Save(UsuarioDto request)
        {
            try
            {
                var model = await GetUsuarioByEmail(request.Email);
                if (model != null)
                    return ErrorResponse(ErrorTypes.NotAllowed, $"Usu�rio com o e-mail: {request.Email} j� cadastrado.");

                model = FactoryUsuario(request.Perfil);

                if (string.IsNullOrEmpty(request.Email))
                    return Null("Obrigat�rio informar o e-mail.");

                if (string.IsNullOrEmpty(request.Nome))
                    return Null("Obrigat�rio informar o nome.");

                if (string.IsNullOrEmpty(request.Senha))
                    return Null("Obrigat�rio informar uma senha.");

                model.Email = request.Email;
                model.Nome = request.Nome;
                model.DicaSecreta = request.DicaSecreta;
                model.PalavraSecreta = request.PalavraSecreta;
                model.Senha = request.Senha;
                model.Perfil = request.Perfil;

                await Save(model);

                return model.MakeResponse();
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        private Usuario FactoryUsuario(Perfis perfil)
        {
            switch (perfil)
            {
                case Perfis.Aluno:
                    return new Aluno();
                case Perfis.Professor:
                    return new Professor();
                case Perfis.Administrador:
                    return new Administrador();
                case Perfis.Convidado:
                default:
                    return new Convidado();
            }
        }

        private async Task<Usuario> Save(Usuario model)
        {
            _context.Usuarios.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                var model = await GetUsuario(id);
                if (model == null)
                    return Null($"n�o encontrado.");

                await DeleteUsuario(id);
                return ResponseDto.Ok();
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        public async Task<ResponseDto> DeleteByEmail(string email)
        {
            try
            {
                var model = await GetUsuarioByEmail(email);
                if (model == null)
                    return Null($"n�o encontrado.");

                await DeleteUsuario(model.ID);
                return ResponseDto.Ok();
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        private async Task DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        private bool Exists(int id)
        {
            return _context.Usuarios.Any(e => e.ID == id);
        }
    }
}