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
using ProjetoIntegrador.Api.Extensions;

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

        // GET: api/Palavras
        [HttpGet]
        public async Task<IActionResult> GetPalavras()
        {
            var dto = new PalavraDto
            {                
                QtdItens = Constants.MAX_PALAVRAS
            };

            return this.SendResponseLista(await service.GetAll(dto));
        }

        // GET: api/Palavras/m/10/1
        [HttpGet("{categoria}/{qtd}")]
        public async Task<IActionResult> GetPalavrasByCategoriaQtd(string categoria, int qtd)
        {
            return await GetPalavras(categoria, qtd, 0);
        }        
        
        // GET: api/Palavras/m/10/1
        [HttpGet("{categoria}/{qtd}/{serie}")]
        public async Task<IActionResult> GetPalavras(string categoria, int qtd, int serie)
        {
            var dto = new PalavraDto
            {
                Categoria = categoria,
                QtdItens = (qtd <= 0 ? Constants.QTD_PALAVRAS : qtd),
                SerieEscolar = serie
            };

            return this.SendResponseLista(await service.GetAll(dto));
        }

        // PUT: api/Palavras/
        [HttpPut("{palavra}/{categoria}")]
        public async Task<IActionResult> PutPalavra(string palavra, string categoria)
        {
            return this.SendResponse(await service.UpdateCategoria(palavra, categoria));
        }

        // POST: api/Palavras
        [HttpPost]
        public async Task<IActionResult> PostPalavra(PalavraRequestDto request)
        {
            return this.SendResponse(await service.Save(request));
        }

        // DELETE: api/Palavras/5
        [HttpDelete("{palavra}")]
        public async Task<IActionResult> DeletePalavra(string palavra)
        {
            return this.SendResponse(await service.DeleteByValor(palavra));
        }
    }
}
