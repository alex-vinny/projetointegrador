using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

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
            if (list.Any() && list.Count > qtd)
            {
                list.Shuffle();
                return list.Take(qtd).ToList();
            }

            return list;
        }

        public static IList<T> Duplicate<T>(this IList<T> list)
        {
            var duplicateList = new List<T>();

            foreach (var item in list)
            {
                Func<T, T> getItem = (x) =>
                {
                    var json = JsonSerializer.Serialize(x);
                    return JsonSerializer.Deserialize<T>(json);
                };

                duplicateList.Add(getItem(item));
                duplicateList.Add(getItem(item));
            }

            return duplicateList;
        }

        public static List<T> ShuffleAndReturn<T>(this IList<T> list)
        {
            list.Shuffle();
            return list.ToList();
        }
    }
}
