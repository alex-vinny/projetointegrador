using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Api.Data;
using ProjetoIntegrador.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseRequest = ProjetoIntegrador.Api.Dtos.RequestDto;
using Request = ProjetoIntegrador.Api.Dtos.CategoriaRequestDto;
using Response = ProjetoIntegrador.Api.Dtos.CategoriaResponseDto;
using ListResponse = ProjetoIntegrador.Api.Dtos.CategoriaItensResponseDto;

namespace ProjetoIntegrador.Api.Services
{
    public class CategoriaService : Service, ICategoriaService
    {
        protected readonly BancoContext _context;

        public CategoriaService(BancoContext context)
        {
            _context = context;
        }

        public async Task<ListResponse> GetAll()
        {
            return await GetAll(BaseRequest.DefaultPagination);
        }

        private async Task<ListResponse> GetAll(BaseRequest dto)
        {
            var response = new CategoriaItensResponseDto();

            try
            {
                var categorias = _context.Categorias
                .OrderBy(c => c.ID)
                .Skip(dto.Skip.Value)
                .Take(dto.Take.Value)
                .Select(c => c.MakeDto());

                response.Categorias = await categorias.ToListAsync();               
            }
            catch (Exception ex)
            {
                return Exception<ListResponse>(ex);
            }

            if(!response.Categorias.Any())
            {
                return Null<ListResponse>("Nenhuma categoria cadastrada.");
            }

            return response;
        }
        
        public async Task<Response> Get(int id)
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
                    return Null<Response>($"Id Categoria: {id} não localizado.");
                }
            }
            catch(Exception ex)
            {
                return Exception<Response>(ex);
            }
        }

        

        public async Task<Response> Save(Request request)
        {
            var response = new Response();
            Models.Categoria data = null;

            try
            {
                data = await _context.Categorias
                    .Where(c => c.Descricao.Equals(request.Categoria, StringComparison.InvariantCultureIgnoreCase))
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                response.AddError(ex.Message, ErrorTypes.BadRequest);
            }

            if (data != null)
                return data.MakeResponse();

            var dto = new CategoriaDto(request);
            var model = dto.ToModel();

            try
            {
                _context.Categorias.Add(model);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.AddError(ex.Message, ErrorTypes.BadRequest);
            }

            if (response.Erros != null)
                return response;

            return model.MakeResponse();
        }       
    }
}