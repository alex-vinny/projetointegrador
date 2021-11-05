using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Models;
using ProjetoIntegrador.Api.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService service;

        public CategoriasController(ICategoriaService context)
        {
            service = context;
        }

        // GET: api/Categorias
        // [HttpGet]
        // public async Task<ActionResult<ResponseDto>> GetCategorias()
        // {
        //     return await service.GetAll();
        // }

        [HttpGet]
        public async Task<ActionResult> GetAllCategoria()
        {

            try
            {
                return Ok(await service.GetAllCategoria());
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Servidor Falhou");
            }
        }

        // POST: api/Categorias        
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> PostCategoria(string categoria)
        {
            var categoriaDto = await service.Save(new CategoriaDto(categoria));

            return Created("PostCategoria", categoriaDto);
        }
    }
}
