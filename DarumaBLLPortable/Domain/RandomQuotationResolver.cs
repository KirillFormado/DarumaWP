using System;
using System.Collections;
using System.Collections.Generic;

namespace DarumaBLLPortable.Domain
{
    public class RandomQuotationResolver
    {
        private IEnumerable _collection;

        public RandomQuotationResolver(IEnumerable collection)
        {
            _collection = collection;
        }

        public KeyValuePair<string, string> RenturnRandomQuotation(int seed)
        {
            var random = new Random(seed);
            var list = new List<KeyValuePair<string, string>>();
            foreach (DictionaryEntry entry in _collection)
            {
                list.Add(new KeyValuePair<string, string>(entry.Key.ToString(), entry.Value.ToString()));
            }
            var quoteNumber = random.Next(list.Count);
            var quoteEntry = list[quoteNumber];
            return quoteEntry;
        }
    }
}
