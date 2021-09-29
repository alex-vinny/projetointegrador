using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Api.Data;
using ProjetoIntegrador.Api.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public class CategoriaService : ICategoriaService
    {
        protected readonly BancoContext _context;

        public CategoriaService(BancoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoriaDto>> GetAll()
        {
            return await GetAll(Dto.Dto.DefaultPagination);
        }

        public async Task<IEnumerable<CategoriaDto>> GetAll(Dto.Dto dto)
        {
            var categorias = _context.Categorias
                .OrderBy(c => c.ID)
                .Skip(dto.Skip)
                .Take(dto.Take)
                .Select(c => c.ToDto());

            return await categorias.ToListAsync();            
        }
        
        public async Task<CategoriaDto> Get(int id)
        {
            var usuario = await _context.Categorias.FindAsync(id);
            if (usuario != null)
                return usuario.ToDto();

            return null;
        }

        public async Task<CategoriaDto> Update(int id, CategoriaDto categoria)
        {
            if (!Exists(id))
            {
                return null;
            }

            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return categoria;
        }

        public async Task<CategoriaDto> Save(CategoriaDto categoria)
        {
            var model = categoria.ToModel();
            _context.Categorias.Add(model);
            await _context.SaveChangesAsync();

            return model.ToDto();
        }

        public async Task<CategoriaDto> Delete(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return null;
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return categoria.ToDto();
        }

        private bool Exists(int id)
        {
            return _context.Categorias.Any(e => e.ID == id);
        }
    }
}