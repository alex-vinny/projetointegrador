namespace ProjetoIntegrador.Api.Dtos.Responses
{
    public class ImagemDto : IModelDto
    {
        public ImagemDto()
        {
        }

        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string Imagem { get; set; }
        public string DescricaoImg { get; set; }
        public int Id { get; set; }

        public ResponseDto MakeResponse()
        {
            return new ResponseDto
            {
                { "id", Id },
                { "descricao", Descricao },
                { "descricaoImg", DescricaoImg },
                { "imagem", Imagem },
                { "categoria", Categoria }
            };
        }
    }
}