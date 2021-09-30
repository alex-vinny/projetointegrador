using Microsoft.AspNetCore.Mvc;
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            var usuarios = await service.GetAll();

            return usuarios.ToList();
        }

        // GET: api/Usuarios/5
        [HttpGet("{email}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(string email)
        {
            var usuario = await service.GetByEmail(email);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        [HttpPut("{email}")]
        public async Task<IActionResult> PutUsuario(string email, UsuarioRequestDto dto)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }

            var usuario = await service.GetByEmail(email);

            if (usuario == null)
            {
                return NotFound();
            }

            await service.Update(usuario.Id, usuario.UpdateFrom(dto));            

            return NoContent();
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> PostUsuario(UsuarioDto usuario)
        {
            usuario = await service.Save(usuario);

            return Created("PostUsuario", usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUsuario(string email)
        {
            var usuario = await service.GetByEmail(email);
            
            if (usuario == null)
            {
                return NotFound();
            }

            await service.Delete(usuario.Id);

            return NoContent();
        }
    }
}
