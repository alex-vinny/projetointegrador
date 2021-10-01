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
    public class PalavraService : Service, IPalavraService
    {
        protected readonly BancoContext _context;

        public PalavraService(BancoContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto> GetByValor(string valor)
        {
            try
            {
                var palavra = await GetPalavraByValor(valor);

                if (palavra == null)
                    return Null($"Palavra: {valor} não cadastrada.");

                return palavra.MakeResponse();
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        private async Task<Palavra> GetPalavraByValor(string valor)
        {
            return await _context.Palavras
                    .Where(c => c.Valor.Equals(valor))
                    .FirstOrDefaultAsync();
        }

        public async Task<ResponseDto> GetAll(PalavraDto request)
        {
            try
            {
                var query = _context.Palavras.AsQueryable();

                if (!string.IsNullOrEmpty(request.Categoria))
                {
                    query = query
                            .Include(c => c.Categoria)
                            .Where(c => c.Categoria.Descricao.Equals(request.Categoria));

                    if (request.SerieEscolar > 0)                    
                        query = query.Where(c => c.SerieEscolar == request.SerieEscolar);                    
                }

                var resultado = query.OrderBy(c => c.ID)
                                    .Take(request.QtdItens)
                                    .Select(c => c.MakeResponse());

                var palavras = await resultado.ToListAsync();

                if (palavras.Any())
                    return new ResponseDto
                    {
                        { "palavras", palavras }
                    };

                return ErrorResponse(ErrorTypes.Null,
                        $"Nenhum palavra cadastrada para categoria: {request.Categoria}",
                        request.SerieEscolar > 0 ? $"Nenhum palavra cadastrada para série escolar: {request.SerieEscolar}" : "");
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
                var palavra = await _context.Palavras.FindAsync(id);
                if (palavra != null)
                    return palavra.MakeResponse();

                return Null($"Palavra com ID: {id} não cadastrada.");
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        public async Task<ResponseDto> UpdateCategoria(string valorPalavra, string descricaoCategoria)
        {
            try
            {
                var palavra = await GetPalavraByValor(valorPalavra);

                if (palavra == null)
                    return Null($"Palavra não localizada para atualização: {valorPalavra}");

                var categoria = await GetCategoria(descricaoCategoria);

                if (categoria == null)
                    return Null($"Categoria não localizada para atualização: {descricaoCategoria}");

                palavra.Categoria = categoria;

                await UpdatePalavra(palavra.ID, palavra);

                return new ResponseDto
                {
                    { "palavra", palavra.MakeResponse() }
                };
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        public async Task<ResponseDto> Update(PalavraRequestDto request)
        {
            try
            {
                var palavra = await GetPalavraByValor(request.Palavra);
                if (palavra == null)
                    return Null($"Palavra não localizada para atualização: {request.Palavra}");

                palavra.Dica = string.IsNullOrEmpty(request.DicaPalavra) ? palavra.Dica : request.DicaPalavra;
                palavra.Valor = string.IsNullOrEmpty(request.Palavra) ? palavra.Valor : request.Palavra;
                palavra.SerieEscolar = request.SerieEscolar > 0 ? request.SerieEscolar : palavra.SerieEscolar;

                await UpdatePalavra(palavra.ID, palavra);

                return new ResponseDto
                {
                    { "palavra",  palavra.MakeResponse() }
                };
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        private async Task<Palavra> UpdatePalavra(int id, Palavra palavra)
        {
            if (Exists(id))
            {
                var data = await _context.Palavras.FindAsync(id);
                _context.Entry(palavra).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return palavra;
        }

        public async Task<ResponseDto> Save(PalavraRequestDto request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Palavra))
                    return Error("Obrigatório informar a palavra.");

                if (string.IsNullOrEmpty(request.DicaPalavra))
                    return Error("Obrigatório informar uma dica.");

                if (string.IsNullOrEmpty(request.Categoria))
                    return Error("Obrigatório informar uma categoria.");

                var palavra = await GetPalavraByValor(request.Palavra);
                
                if (palavra != null)
                {
                    return new ResponseDto
                    {
                        { "palavra", palavra.MakeResponse() }
                    };
                }
                else
                {
                    palavra = new Palavra();                    
                }

                palavra.Dica = request.DicaPalavra;
                palavra.Valor = request.Palavra;
                palavra.SerieEscolar = request.SerieEscolar;

                var categoria = await GetCategoria(request.Categoria);

                if (categoria != null)
                {
                    palavra.Categoria = categoria;
                }
                else
                {
                    palavra.Categoria = new Categoria
                    {
                        Descricao = request.Categoria
                    };
                }

                _context.Palavras.Add(palavra);
                await _context.SaveChangesAsync();

                return palavra.MakeResponse();
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        private async Task<Categoria> GetCategoria(string descricao)
        {
            return await _context.Categorias
                .Where(c => c.Descricao.Equals(descricao))
                .FirstOrDefaultAsync();
        }

        public async Task<ResponseDto> DeleteByValor(string valor)
        {
            try
            {
                var model = await GetPalavraByValor(valor);
                if(model != null)
                    return await Delete(model.ID);

                return Null("Não encontrado");
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                if (Exists(id))
                {
                    await DeletePalavra(id);
                    return ResponseDto.Ok();
                }

                return Null("Não encontrado");
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }

        private async Task DeletePalavra(int id)
        {
            var palavra = await _context.Palavras.FindAsync(id);
            if (palavra != null)
            {
                _context.Palavras.Remove(palavra);
                await _context.SaveChangesAsync();
            }
        }

        private bool Exists(int id)
        {
            return _context.Palavras.Any(e => e.ID == id);
        }
    }
}