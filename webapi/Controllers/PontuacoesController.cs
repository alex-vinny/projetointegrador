using Microsoft.AspNetCore.Mvc;
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Extensions;
using ProjetoIntegrador.Api.Services;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PontuacoesController : ControllerBase
    {
        private readonly IPontucaoService service;

        public PontuacoesController(IPontucaoService service)
        {
            this.service = service;
        }

        // PUT: api/Pontuacoes/5        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPontuacao(int id, PontuacaoRequestDto dto)
        {
            return this.SendResponse(await service.Update(id, dto));
        }

        // POST: api/Pontuacoes
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> PostPontuacao(PontuacaoCreationRequestDto dto)
        {
            return this.SendResponse(await service.Save(dto));
        }
    }
}
