using Microsoft.AspNetCore.Mvc;
using ProjetoIntegrador.Api.Config;
using ProjetoIntegrador.Api.Extensions;
using ProjetoIntegrador.Api.Services;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagensController : ControllerBase
    {
        private readonly IImagemService service;

        public ImagensController(IImagemService context)
        {
            service = context;
        }

        // GET: api/Imagens/
        [HttpGet]
        public async Task<IActionResult> GetlImagens(string categoria, int? quantidade = null)
        {
            quantidade = quantidade ?? Constants.QTD_IMAGENS;
            return this.SendResponseLista(await service.GetByCategoria(categoria, quantidade.Value));
        }
    }
}
