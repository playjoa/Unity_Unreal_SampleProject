using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.Extensions
{
    public static class DictionaryExtensions
    {
        private static readonly Random Rng = new Random();
        
        public static TValue RandomValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return dictionary.ElementAt(Rng.Next(dictionary.Count)).Value;
        }
        
        public static TKey RandomKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return dictionary.ElementAt(Rng.Next(dictionary.Count)).Key;
        }

        public static void IncrementDictioCount<TKey>(this Dictionary<TKey, int> dictionary, TKey key, int countAmount = 1)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] += countAmount;
            }
            else
            {
                dictionary.Add(key, countAmount);
            }
        }

        public static void DecrementDictioCount<TKey>(this Dictionary<TKey, int> dictionary, TKey key, int decrementAmount = -1)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] -= decrementAmount;
            }
            else
            {
                dictionary.Add(key, decrementAmount);
            }
        }
    }
}