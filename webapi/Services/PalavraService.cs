using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Api.Data;
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public class PalavraService : IPalavraService
    {
        protected readonly BancoContext _context;

        public PalavraService(BancoContext context)
        {
            _context = context;
        }
        
        public async Task<PalavraDto> GetByValor(string palavra)
        {
            var p = await _context.Palavras
                .Where(c => c.Valor.Equals(palavra, System.StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefaultAsync();

            return p.ToDto();
        }

        public async Task<IEnumerable<PalavraDto>> GetAll(PalavraDto palavra)
        {
            var palavras = _context.Palavras.AsQueryable();

            if(!string.IsNullOrEmpty(palavra.Categoria))
            {
                palavras = palavras.Include(c => c.Categoria)
                    .Where(c => c.Categoria.Descricao.Equals(palavra.Categoria, StringComparison.InvariantCultureIgnoreCase));
            }
            
            var resultado = palavras.OrderBy(c => c.ID)                                
                                .Take(palavra.Take.Value)
                                .Select(c => c.ToDto());

            return await resultado.ToListAsync();            
        }
        
        public async Task<PalavraDto> Get(int id)
        {
            var palavra = await _context.Palavras.FindAsync(id);
            if (palavra != null)
                return palavra.ToDto();

            return null;
        }

        public async Task UpdateCategoria(string palavra, string categoria)
        {
            var p = await GetByValor(palavra);
            var c = await GetCategoria(categoria);

            if(c != null)
            {
                p.CategoriaId = c.ID;
                p.Categoria = c.Descricao;
            }

            await Update(p.Id.Value, p);
        }

        public async Task Update(int id, PalavraDto palavra)
        {
            if (Exists(id))
            {
                var data = await _context.Palavras.FindAsync(id);
                _context.Entry(data.UpdateFrom(palavra)).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PalavraDto> Save(PalavraDto palavra)
        {
            var data = await GetByValor(palavra.Palavra);
            if (data != null)
                return data;

            var c = await GetCategoria(palavra.Categoria);
            
            if (c != null)
            {
                palavra.CategoriaId = c.ID;
                palavra.Categoria = c.Descricao;
            }
            else
            {
                palavra.CategoriaId = 0;
            }

            var model = palavra.ToModel();
            _context.Palavras.Add(model);
            await _context.SaveChangesAsync();

            return model.ToDto();
        }

        private async Task<Categoria> GetCategoria(string descricao)
        {
            return await _context.Categorias
                .Where(c => c.Descricao.Equals(descricao, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefaultAsync();
        }

        public async Task Delete(int id)
        {
            var palavra = await _context.Palavras.FindAsync(id);
            if (palavra != null)
            {
                _context.Palavras.Remove(palavra);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteByValor(string valor)
        {
            var dto = await GetByValor(valor);
            if (dto != null)
            {
                await Delete(dto.Id.Value);
            }
        }

        private bool Exists(int id)
        {
            return _context.Categorias.Any(e => e.ID == id);
        }
    }
}