using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.Extensions
{
    public static class ListExtensions
    {
        private static readonly Random Rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            var listCount = list.Count;
            while (listCount > 1)
            {
                listCount--;
                var k = Rng.Next(listCount + 1);
                (list[k], list[listCount]) = (list[listCount], list[k]);
            }
        }
        
        public static T RandomElement<T>(this IList<T> list)
        {
            if (list == null || !list.Any()) return default;
            
            return list[Rng.Next(list.Count)];
        }

        public static T RandomElement<T>(this T[] array)
        {
            if (array == null || !array.Any()) return default;
            
            return array[Rng.Next(array.Length)];
        }
    }
}