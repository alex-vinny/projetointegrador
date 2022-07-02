using System.Collections.Generic;

namespace ProjetoIntegrador.Api.Dtos.Responses.Cms
{
    public class ImagensCms
    {
        public ImagensCms()
        {
            modeloImagens = new List<ImagemCms>();
        }
        public List<ImagemCms> modeloImagens { get; set; }
    }
}