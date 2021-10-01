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
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService service;

        public UsuariosController(IUsuarioService context)
        {
            service = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetUsuarios()
        {
            return this.SendResponse(await service.GetAll());
        }

        // GET: api/Usuarios/5
        [HttpGet("{email}")]
        public async Task<ActionResult<ResponseDto>> GetUsuario(string email)
        {
            return this.SendResponse(await service.GetByEmail(email));
        }

        // PUT: api/Usuarios/5
        [HttpPut("{email}")]
        public async Task<IActionResult> PutUsuario(string email, UsuarioRequestDto dto)
        {
            return this.SendResponse(await service.Update(email, dto));
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> PostUsuario(UsuarioDto usuario)
        {
            return this.SendResponse(await service.Save(usuario));
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUsuario(string email)
        {
            return this.SendResponse(await service.DeleteByEmail(email));
        }
    }
}
