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

        // GET: api/Palavras/m/10/1
        [HttpGet("{categoria}/{qtd}/{serie}")]
        public async Task<ActionResult<ResponseDto>> GetPalavras(string categoria, int qtd, int serie = 0)
        {
            var dto = new PalavraDto
            {
                Categoria = categoria,
                QtdItens = (qtd <= 0 ? Constants.QTD_PALAVRAS : qtd),
                SerieEscolar = serie
            };

            return this.SendResponse(await service.GetAll(dto));
        }

        // PUT: api/Palavras/
        [HttpPut("{palavra}/{categoria}")]
        public async Task<IActionResult> PutPalavra(string palavra, string categoria)
        {
            return this.SendResponse(await service.UpdateCategoria(palavra, categoria));
        }

        // POST: api/Palavras
        [HttpPost]
        public async Task<ActionResult<PalavraDto>> PostPalavra(PalavraRequestDto request)
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
