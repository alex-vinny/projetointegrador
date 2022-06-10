using Microsoft.AspNetCore.Mvc;
using ProjetoIntegrador.Api.Config;
using ProjetoIntegrador.Api.Extensions;
using ProjetoIntegrador.Api.Services;
using System.Threading.Tasks;

namespace api.Controllers
{
    /// <summary>
    /// Controller para expor recursos de imagens.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ImagensController : ControllerBase
    {
        private readonly IImagemService service;

        public ImagensController(IImagemService context)
        {
            service = context;
        }

        /// <summary>
        /// GET: api/Imagens/
        /// </summary>
        /// <param name="categoria">Opcional: Código ou descrição da Categoria.</param>
        /// <param name="quantidade">Opcional: Valor padrão é 20.</param>
        /// <returns>Lista de imagens.</returns>
        [HttpGet]
        public async Task<IActionResult> GetlImagens(string categoria, int? quantidade = null)
        {
            quantidade = quantidade ?? Constants.QTD_IMAGENS;
            return this.SendResponseLista(await service.GetByCategoria(categoria, quantidade.Value));
        }
    }
}
