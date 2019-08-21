using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRS.Infrastructure.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        /// <summary>
        /// Wraps object instance into an IEnumerable&lt;T&gt;
        /// consisting of a single item.
        /// </summary>
        /// <typeparam name="T"> Type of the object. </typeparam>
        /// <param name="item"> The instance that will be wrapped. </param>
        /// <returns> An IEnumerable&lt;T&gt; consisting of a single item. </returns>
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }

        // DivideBy, DemarcateBy
        public static List<List<TSource>> ToUniqueSetsBy<TSource, TKey>(this IEnumerable<TSource> collection, Func<TSource, TKey> keySelector)
        {
            var grouped = collection
                .GroupBy(keySelector)
                .Select(x => new Stack<TSource>(x));

            var listOfUniqueOnes = new List<List<TSource>>();

            while (grouped.Any())
            {
                listOfUniqueOnes.Add(grouped.Select(x => x.Pop()).ToList());
                grouped = grouped.Where(x => x.Any()).ToList();
            }

            return listOfUniqueOnes;
        }

        private static List<List<TSource>> ToUniqueSetsBy1<TSource, TKey>(this IEnumerable<TSource> collection, Func<TSource, TKey> keySelector)
        {
            var groupedPrices = collection
                .GroupBy(keySelector)
                .SelectMany(g => g.Select((x, i) => (x, i)))
                .GroupBy(x => x.i)
                .Select(g => g.Select(gg => gg.x).ToList()).ToList();
            return groupedPrices;
        }
    }
}
