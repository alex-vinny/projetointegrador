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
    public class UsuarioService : IUsuarioService
    {
        protected readonly BancoContext _context;

        public UsuarioService(BancoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAll()
        {
            return await GetAll(RequestDto.DefaultPagination);
        }

        public async Task<IEnumerable<UsuarioDto>> GetAll(RequestDto dto)
        {
            var usuarios = _context.Usuarios
                .OrderBy(c => c.ID)
                .Skip(dto.Skip.Value)
                .Take(dto.Take.Value)
                .Select(c => c.ToDto());

            return await usuarios.ToListAsync();            
        }
        
        public async Task<UsuarioDto> Get(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
                return usuario.ToDto();

            return null;
        }

        public async Task<UsuarioDto> GetByEmail(string email)
        {
            var usuario = await _context.Usuarios
                            .Where(c => c.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase))
                            .FirstOrDefaultAsync();
            if (usuario != null)
                return usuario.ToDto();

            return null;
        }

        public async Task Update(int id, UsuarioDto usuario)
        {
            if (Exists(id))
            {
                var entity = await _context.Usuarios.FindAsync(id);
                _context.Entry(entity).State = EntityState.Modified;                
                _context.Usuarios.Update(entity.UpdateFrom(usuario));

                await _context.SaveChangesAsync();
            }
        }

        public async Task<UsuarioDto> Save(UsuarioDto usuario)
        {
            var model = usuario.ToModel();
            _context.Usuarios.Add(model);
            await _context.SaveChangesAsync();

            return model.ToDto();
        }

        public async Task Delete(int id)
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