using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Api.Config;
using ProjetoIntegrador.Api.Data;
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Extensions;
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
                    return Null($"Palavra: {valor} n�o cadastrada.");

                return palavra.MakeResponse();
            }
            catch (Exception ex)
            {
                return Exception(ex);
            }
        }
        
        private async Task<Palavra> GetPalavraByValor(string valor)
        {
            valor = valor.RemoverAcentos();
            return await _context.Palavras
                    .Where(c => c.ValorSemAcento.Equals(valor))
                    .FirstOrDefaultAsync();
        }

        public async Task<List<ResponseDto>> GetAll(PalavraDto request)
        {
            try
            {
                var query = _context.Palavras.AsQueryable();
                query = query.Include(c => c.Categoria);

                if(request.InformarCategoria && string.IsNullOrEmpty(request.Categoria))
                {
                    return new[]
                    {
                        ErrorResponse(ErrorTypes.Null, "Obrigatório informar a categoria para palavra.")
                    }
                    .ToList();
                }

                if (!string.IsNullOrEmpty(request.Categoria))
                {
                    request.Categoria = request.Categoria.RemoverAcentos(true);
                    query = query.Where(c => c.Categoria.DescricaoSemAcento.Equals(request.Categoria));

                    if (request.SerieEscolar > 0)                    
                        query = query.Where(c => c.SerieEscolar == request.SerieEscolar);                    
                }              

                bool ordenarAcendent = (new Random().Next(0, 1) == 1 ? true : false);

                if (ordenarAcendent)
                    query = query.OrderBy(c => c.ValorSemAcento);
                else
                    query = query.OrderBy(c => c.ValorSemAcento);

                var resultado = query.Select(c => c.MakeResponse());

                var palavras = await resultado.ToListAsync();

                if (palavras.Any())
                {
                    if(request.QtdItens == Constants.MAX_PALAVRAS || request.QtdItens >= palavras.Count)
                        return palavras;

                    return palavras.TakeAfterShuffle(request.QtdItens);
                }

                var erro = ErrorResponse(ErrorTypes.Null,
                        $"Nenhum palavra cadastrada para categoria: {request.Categoria}",
                        request.SerieEscolar > 0 ? $"Nenhum palavra cadastrada para s�rie escolar: {request.SerieEscolar}" : "");
                
                return new[]
                {
                    erro
                }
                .ToList();
            }
            catch (Exception ex)
            {
                return new[] { Exception(ex) }.ToList();
            }
        }
        
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                var palavra = await _context.Palavras.FindAsync(id);
                if (palavra != null)
                    return palavra.MakeResponse();

                return Null($"Palavra com ID: {id} n�o cadastrada.");
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
                    return Null($"Palavra n�o localizada para atualiza��o: {valorPalavra}");

                var categoria = await GetCategoria(descricaoCategoria);

                if (categoria == null)
                    return Null($"Categoria n�o localizada para atualiza��o: {descricaoCategoria}");

                palavra.Categoria = categoria;

                await UpdatePalavra(palavra.ID, palavra);

                return palavra.MakeResponse();
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
                    return Null($"Palavra n�o localizada para atualiza��o: {request.Palavra}");

                palavra.Dica = string.IsNullOrEmpty(request.DicaPalavra) ? palavra.Dica : request.DicaPalavra;
                palavra.DicaSemAcento = palavra.Dica.RemoverAcentos();
                palavra.Valor = string.IsNullOrEmpty(request.Palavra) ? palavra.Valor : request.Palavra;
                palavra.ValorSemAcento = palavra.Valor.RemoverAcentos();
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
                    return Error("Obrigat�rio informar a palavra.");

                if (string.IsNullOrEmpty(request.DicaPalavra))
                    return Error("Obrigat�rio informar uma dica.");

                if (string.IsNullOrEmpty(request.Categoria))
                    return Error("Obrigat�rio informar uma categoria.");

                var palavra = await GetPalavraByValor(request.Palavra);
                
                if (palavra != null)
                {
                    return palavra.MakeResponse();
                }
                else
                {
                    palavra = new Palavra();                    
                }

                palavra.Dica = request.DicaPalavra;
                palavra.DicaSemAcento = request.DicaPalavra.RemoverAcentos();
                palavra.Valor = request.Palavra;
                palavra.ValorSemAcento = request.Palavra.RemoverAcentos();
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
                        Descricao = request.Categoria.RemoverAcentos(true)
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
            descricao = descricao.RemoverAcentos(true);
            return await _context.Categorias
                .Where(c => c.DescricaoSemAcento.Equals(descricao))
                .FirstOrDefaultAsync();
        }

        public async Task<ResponseDto> DeleteByValor(string valor)
        {
            try
            {
                var model = await GetPalavraByValor(valor);
                if(model != null)
                    return await Delete(model.ID);

                return Null("N�o encontrado");
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

                return Null("N�o encontrado");
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