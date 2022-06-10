using ProjetoIntegrador.Api.Config;
using ProjetoIntegrador.Api.Data;
using ProjetoIntegrador.Api.Dtos;
using ProjetoIntegrador.Api.Dtos.Requests.Cms;
using ProjetoIntegrador.Api.Dtos.Responses;
using ProjetoIntegrador.Api.Dtos.Responses.Cms;
using ProjetoIntegrador.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjetoIntegrador.Api.Services
{
    public class ImagemService : Service, IImagemService
    {
        private readonly BancoContext _context;
        private readonly ICategoriaService _service;
        private readonly IHttpClientFactory _clientFactory;
        private readonly Configurator _configurator;

        public ImagemService(BancoContext context, 
            ICategoriaService service,
            IHttpClientFactory clientFactory,
            Configurator configurator)
        {
            _context = context;
            _service = service;
            _clientFactory = clientFactory;
            _configurator = configurator;
        }

        public async Task<List<ResponseDto>> GetByCategoria(string categoria, int quantidade)
        {
            int categoriaId = 0;

            if (!string.IsNullOrEmpty(categoria))
            {
                if (!categoria.IsNumeric())
                {
                    var categoriaItem = await _service.GetByDescricao(categoria);
                    if (categoriaItem["id"] == null)
                        return categoriaItem.AsList();

                    if (!int.TryParse(categoriaItem["id"].ToString(), out categoriaId))
                        return Null($"Categoria {categoria} não cadastrada.").AsList();
                }
                else
                {
                    if (!int.TryParse(categoria, out categoriaId))
                        return Null($"Categoria {categoria} não é um número inteiro.").AsList();
                }
            }

            return await GetImagensAsResponse(quantidade, categoria: categoriaId);
        }

        public async Task<List<ResponseDto>> GetImagensAsResponse(int take, int? categoria = null)
        {
            var imagens = await GetImagens(take, categoria);
            
            if (imagens == null || imagens.Count == 0)
                return Null($"Nenhuma imagem encontrada.").AsList();
            
            return imagens
                .Select(c => c.MakeResponse())
                .ToList();
        }

        public async Task<List<ImagemDto>> GetImagens(int take, int? categoria = null)
        {
            var query = new ImagemQueryRequestDto(take, categoria);

            var body = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

            using var client = _clientFactory.CreateClient();
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            var response = await client.PostAsync(_configurator.Get(Constants.CMS_CONFIG), body);

            response.EnsureSuccessStatusCode();
            
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var cmsImagens = await JsonSerializer.DeserializeAsync<Cms<ImagensCms>>(responseStream);

            return cmsImagens.MakeResponse(take);
        }
    }
}