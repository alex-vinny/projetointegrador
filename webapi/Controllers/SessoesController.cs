using Microsoft.AspNetCore.Mvc;
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Extensions;
using ProjetoIntegrador.Api.Services;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessoesController : ControllerBase
    {
        private readonly ISessaoService service;

        public SessoesController(ISessaoService service)
        {
            this.service = service;
        }

        // PUT: api/Sessoes/5        
        [HttpPut("{email}")]
        public async Task<IActionResult> PutSessao(string email, SessaoRequestDto sessao)
        {
            return this.SendResponse(await service.Update(email, sessao));
        }

        // POST: api/Sessoes
        [HttpPost("{email}")]
        public async Task<ActionResult<ResponseDto>> PostSessao(string email, SessaoDto sessao)
        {
            return this.SendResponse(await service.Save(email, sessao));
        }
    }
}
