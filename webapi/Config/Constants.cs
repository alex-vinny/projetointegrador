namespace ProjetoIntegrador.Api.Config
{
    public static class Constants
    {
        public readonly static string DATABASE_ENV = "DATABASE_URL";
        public readonly static string DATABASE_SETT = "DefaultConnection";
        public readonly static int QTD_PALAVRAS = 30;
        public readonly static int MAX_PALAVRAS = -1;
        public readonly static int TAMANHO_PAGINA = 5000;
        public readonly static string CMS_CONFIG = "CmsUrlApi";
        public readonly static int QTD_IMAGENS = 20;
        public readonly static string CONFIG_DEFAULT_MSG = "Configuração {0} não encontrada.";
        public readonly static string CMS_FILTER_SYMBOL = "###";
        public readonly static string CMS_IMAGENS_QUERY = @"
query {
  modeloImagens(###) {
    id
    item {      
      codigo
      descricao
      url {        
        fileName
        url
      }
      categoria {        
        codigo
        descricao
      }
    }
  }
}
";
    }
}
