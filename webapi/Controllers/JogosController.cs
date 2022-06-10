using Microsoft.AspNetCore.Mvc;
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoIntegrador.Api.Extensions;

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

        // GET: api/Usuarios
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            return this.SendResponseLista(await service.GetAll());
        }
    }
}
