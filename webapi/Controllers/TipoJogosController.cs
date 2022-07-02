using Microsoft.AspNetCore.Mvc;
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegrador.Api.Extensions;
using ProjetoIntegrador.Api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoJogosController : ControllerBase
    {
        private readonly ITipoJogoService service;

        public TipoJogosController(ITipoJogoService context)
        {
            service = context;
        }

        // GET: api/TipoJogos
        [HttpGet]
        public async Task<IActionResult> GetJogos()
        {
            return this.SendResponseLista(await service.GetAll());
        }

        // GET: api/TipoJogos/{codigo}
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetJogo(TiposJogo codigo)
        {
            var jogoDto = await service.GetJogo(new JogoRequestDto(codigo));

            return this.SendResponse(jogoDto);
        }
    }
}
