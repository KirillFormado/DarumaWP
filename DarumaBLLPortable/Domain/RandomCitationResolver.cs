using System;
using System.Collections;
using System.Collections.Generic;

namespace DarumaBLLPortable.Domain
{
    public class RandomCitationResolver
    {
        private IEnumerable _collection;

        public RandomCitationResolver(IEnumerable collection)
        {
            _collection = collection;
        }

        public string RenturnRandomCitation(int seed)
        {
            var random = new Random(seed);
            var list = new List<string>();
            foreach (DictionaryEntry entry in _collection)
            {
                list.Add(entry.Value.ToString());
            }
            var citationNumber = random.Next(list.Count);

            var text = list[citationNumber];
            return text;
        }
    }
}
