using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Api.Data;
using ProjetoIntegrador.Api.Dto;
using System.Collections.Generic;
using System.Linq;
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
            return await GetAll(Dto.Dto.DefaultPagination);
        }

        public async Task<IEnumerable<UsuarioDto>> GetAll(Dto.Dto dto)
        {
            var usuarios = _context.Usuarios
                .OrderBy(c => c.ID)
                .Skip(dto.Skip)
                .Take(dto.Take)
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

        public async Task<UsuarioDto> Update(int id, UsuarioDto usuario)
        {
            if (!Exists(id))
            {
                return null;
            }

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioDto> Save(UsuarioDto usuario)
        {
            var model = usuario.ToModel();
            _context.Usuarios.Add(model);
            await _context.SaveChangesAsync();

            return model.ToDto();
        }

        public async Task<UsuarioDto> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return null;
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario.ToDto();
        }

        private bool Exists(int id)
        {
            return _context.Usuarios.Any(e => e.ID == id);
        }
    }
}