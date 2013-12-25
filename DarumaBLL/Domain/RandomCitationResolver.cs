using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Phone.Info;

namespace DarumaBLL.RandomCitationUseCase
{
    public class RandomCitationResolver
    {
        private IEnumerable _collection;

        public RandomCitationResolver(IEnumerable collection)
        {
            _collection = collection;
        }

        public string RenturnRandomCitation()
        {
            var seed = DateTime.Now.Second
                       + (int) DeviceStatus.ApplicationCurrentMemoryUsage;

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
