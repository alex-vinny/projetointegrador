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
        private readonly ICategoriaService _service;
        private readonly IHttpClientFactory _clientFactory;
        private readonly Configurator _configurator;

        public ImagemService(ICategoriaService service,
            IHttpClientFactory clientFactory,
            Configurator configurator)
        {
            _service = service;
            _clientFactory = clientFactory;
            _configurator = configurator;
        }

        public async Task<List<ResponseDto>> GetByCategoria(string categoria, int quantidade)
        {
            ResponseDto categoriaItem = null;

            if (!string.IsNullOrEmpty(categoria))
            {
                if (!categoria.IsNumeric())
                {
                    categoriaItem = await _service.GetByDescricao(categoria);
                    if (categoriaItem["id"] == null)
                        return categoriaItem.AsList();

                    if (!int.TryParse(categoriaItem["id"].ToString(), out _))
                        return Null($"Categoria {categoria} não cadastrada.").AsList();
                }
                else
                {
                    if (!int.TryParse(categoria, out _))
                        return Null($"Código da categoria {categoria} não é um número inteiro.").AsList();
                }
            }

            return await GetImagensAsResponse(quantidade, categoriaDto: categoriaItem);
        }

        public async Task<List<ResponseDto>> GetImagensAsResponse(int take, ResponseDto categoriaDto = null)
        {
            int? categoria = null;
            if (categoriaDto != null)
            {
                if (int.TryParse(categoriaDto["id"].ToString(), out int id))
                    categoria = id;
            }

            var imagens = await GetImagens(take, categoria);
            
            if (imagens == null || imagens.Count == 0)
            {
                var msg = $"Nenhuma imagem encontrada{(categoriaDto != null ? " para a categoria " + categoriaDto["descricao"].ToString() : "")}.";
                return Null(msg).AsList();
            }

            return imagens
                .Select(c => c.MakeResponse())
                .ToList();
        }

        public async Task<List<ImagemDto>> GetImagens(int take, int? categoria = null)
        {
            var query = new ImagemQueryRequestDto(take, categoria);

            var body = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

            using var client = _clientFactory.CreateClient();

            var response = await client.PostAsync(_configurator.Get(Constants.CMS_CONFIG), body);

            response.EnsureSuccessStatusCode();
            
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var cmsImagens = await JsonSerializer.DeserializeAsync<Cms<ImagensCms>>(responseStream);

            var list = cmsImagens.MakeResponse(take, false);

            return list
                .Duplicate()
                .ShuffleAndReturn();
        }
    }
}