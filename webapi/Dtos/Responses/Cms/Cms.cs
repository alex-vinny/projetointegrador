namespace ProjetoIntegrador.Api.Dtos.Responses.Cms
{
    public class Cms<T>
        where T : class, new()
    {
        public Cms()
        {
            data = new T();
        }
        public T data { get; set; }
    }
}