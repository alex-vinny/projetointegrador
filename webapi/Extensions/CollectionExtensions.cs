using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoIntegrador.Api.Extensions
{
    public static class CollectionExtensions
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static List<T> TakeAfterShuffle<T>(this List<T> list, int qtd)
        {
            if(list.Any() && list.Count > qtd)
            {
                list.Shuffle();
                return list.Take(qtd).ToList();
            }

            return list;
        }
    }
}
