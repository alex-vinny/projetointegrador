namespace ProjetoIntegrador.Api.Extensions
{
    public static class StringExtensions
    {
        public static string RemoverAcentos(this string texto, bool retirarEspacos = false)
        {
            if (string.IsNullOrEmpty(texto))
                return "";

            string com = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string sem = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
            
            for (int i = 0; i < com.Length; i++)
            {
                texto = texto.Replace(com[i], sem[i]);
            }          

            if (retirarEspacos)
            {
                texto = texto.Replace(" ", "");
            }

            return texto.ToUpper();
        }
    }
}
