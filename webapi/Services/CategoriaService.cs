using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Api.Data;
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Extensions;
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

        public async Task<ResponseDto> GetAll()
        {
            return await GetAll(BaseRequest.DefaultPagination);
        }

        private async Task<ResponseDto> GetAll(BaseRequest dto)
        {
            try
            {
                var query = _context.Categorias
                    .OrderBy(c => c.ID)
                    .Skip(dto.Skip.Value)
                    .Take(dto.Take.Value)
                    .Select(c => c.MakeResponse());

                var categorias = await query.ToListAsync();

                if (!categorias.Any())
                    return Null("Nenhuma categoria cadastrada.");

                return new ResponseDto
                {
                    { "categorias", categorias }
                };
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
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
                    return Null($"Id Categoria: {id} não localizado.");
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
                return Null("Obrigatório informar valor válido como categoria.");
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