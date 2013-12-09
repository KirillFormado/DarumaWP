using System;
using System.Collections;
using System.Collections.Generic;

namespace DarumaBLL.RandomCitationUseCase
{
    public class RandomCitationResolver
    {
        private IEnumerable _collection;
        private Random _random = new Random();

        public RandomCitationResolver(IEnumerable collection)
        {
            _collection = collection;
        }

        public string RenturnRandomCitation()
        {
            var list = new List<string>();
            foreach (DictionaryEntry entry in _collection)
            {
                list.Add(entry.Value.ToString());
            }
            var citationNumber = _random.Next(list.Count);

            var text = list[citationNumber];
            return text;
        }
    }
}
