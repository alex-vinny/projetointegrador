using ProjetoIntegrador.Api.Config;

namespace ProjetoIntegrador.Api.Dtos.Requests.Cms
{
    public class ImagemQueryRequestDto
    {
        public ImagemQueryRequestDto(int quantidade, int? categoria)
        {
            string filter = $"first: {quantidade * 3}, orderBy: id_ASC, stage: PUBLISHED";
            if (categoria > 0)
                filter += ", where: { item: { categoria: { codigo: " + categoria.Value + "} } }";

            Query = GetQuery(filter);
        }
        
        public string Query { get; }

        private string GetQuery(string filter)
        {
            return Constants.CMS_IMAGENS_QUERY.Replace(Constants.CMS_FILTER_SYMBOL, filter);
        }
    }
    
}
