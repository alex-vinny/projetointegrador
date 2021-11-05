using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Api.Data;
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Extensions;
using ProjetoIntegrador.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseRequest = ProjetoIntegrador.Api.Dtos.RequestDto;

namespace ProjetoIntegrador.Api.Services
{
    public class CategoriaService : Service, ICategoriaService
    {
        protected readonly BancoContext _context;

        public CategoriaService(BancoContext context)
        {
            _context = context;
        }

        public async Task<List<ResponseDto>> GetAll()
        {
            return await GetAll(BaseRequest.DefaultPagination);
        }

        private async Task<List<ResponseDto>> GetAll(BaseRequest dto)
        {
            try
            {
                var query = _context.Categorias
                    .OrderBy(c => c.Descricao)
                    .Skip(dto.Skip.Value)
                    .Take(dto.Take.Value)
                    .Select(c => c.MakeResponse());

                var categorias = await query.ToListAsync();

                if (!categorias.Any())
                    return new[] { Null("Nenhuma categoria cadastrada.") }.ToList();

                return categorias;
            }
            catch (Exception ex)
            {
                return new[] { Exception(ex) }.ToList();
            }
        }
        
        public async Task<Categoria[]> GetAllCategoria()
        {
            return await GetAllCategorias();
        }
        
        private async Task<Categoria[]> GetAllCategorias()
        {
            return await _context.Categorias
                    .Where(c => c.Descricao != null)
                    .ToArrayAsync(); 
        }
        
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                var categoria = await _context.Categorias.FindAsync(id);
                if (categoria != null)
                {
                    return categoria.MakeResponse();
                }
                else
                {
                    return Null($"Id Categoria: {id} n�o localizado.");
                }
            }
            catch(Exception ex)
            {
                return Exception(ex);
            }
        }

        public async Task<ResponseDto> Save(CategoriaDto request)
        {
            Models.Categoria data = null;

            if (string.IsNullOrEmpty(request.Categoria))
            {
                return Null("Obrigat�rio informar valor v�lido como categoria.");
            }

            try
            {
                request.Categoria = request.Categoria.RemoverAcentos(true);
                data = await _context.Categorias
                    .Where(c => c.DescricaoSemAcento.Equals(request.Categoria))
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }

            if (data != null)
                return data.MakeResponse();

            var model = request.ToModel();

            try
            {
                _context.Categorias.Add(model);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }

            return model.MakeResponse();
        }     
    }
}