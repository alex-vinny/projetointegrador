using System.Collections;

namespace ProjetoIntegrador.Api.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool Exists<TKey>(this IDictionary dictionary, TKey key)
        {
            return dictionary.Contains(key);
        }
    }
}
