using ProjetoIntegrador.Api.Dtos.Responses;
using ProjetoIntegrador.Api.Dtos.Responses.Cms;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoIntegrador.Api.Extensions
{
    public static class CmsResponseDtoExtensions
    {
        public static List<ImagemDto> MakeResponse(this Cms<ImagensCms> cms, int take, bool shuffle = true)
        {
            var list = new List<ImagemDto>();

            foreach (var item in cms.data.modeloImagens)
            {
                var imagem = new ImagemDto
                {
                    Descricao = item.item.descricao,
                    DescricaoImg = item.item.url.fileName,
                    Imagem = item.item.url.url,
                    Id = item.item.codigo,
                    Categoria = item.item.categoria.descricao
                };
                list.Add(imagem);
            }

            if (shuffle)
                return list.TakeAfterShuffle(take);

            return list
                .Take(take)
                .ToList();
        }
    }
}
