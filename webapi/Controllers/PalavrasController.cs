using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Api.Config;
using ProjetoIntegrador.Api.Data;
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Models;
using ProjetoIntegrador.Api.Services;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalavrasController : ControllerBase
    {
        private readonly IPalavraService service;

        public PalavrasController(IPalavraService context)
        {
            service = context;
        }

        // GET: api/Palavras/m/10/1
        [HttpGet("{categoria}/{qtd}/{serie}")]
        public async Task<ActionResult<IEnumerable<PalavraDto>>> GetPalavras(string categoria, int qtd, string serie)
        {
            var palavras = await service.GetAll(
                new PalavraDto 
                { 
                    Categoria = categoria, 
                    Take = (qtd <= 0? Constants.QTD_PALAVRAS : qtd)
                });

            if(palavras.Any())
                return palavras.ToList();

            return NotFound();
        }

        // PUT: api/Categorias/
        [HttpPut("{palavra}/{categoria}")]
        public async Task<IActionResult> PutPalavra(string palavra, string categoria)
        {
            await service.UpdateCategoria(palavra, categoria);

            return NoContent();
        }

        // POST: api/Palavras
        [HttpPost]
        public async Task<ActionResult<PalavraDto>> PostPalavra(PalavraDto palavra)
        {
            palavra = await service.Save(palavra);

            return Created("PostPalavra", palavra);
        }

        // DELETE: api/Palavras/5
        [HttpDelete("{palavra}")]
        public async Task<IActionResult> DeletePalavra(string palavra)
        {
            await service.DeleteByValor(palavra);

            return NoContent();
        }
    }
}
