using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarumaBLLPortable.ApplicationServices.Abstractions;
using DarumaBLLPortable.ApplicationServices.Entites;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.ApplicationServices.Services
{
    public class FavoritApplicationService : IFavoritApplicationService
    {
        private readonly IFavoritStorage _favoritStorage;

        public FavoritApplicationService(IFavoritStorage favoritStorage)
        {
            _favoritStorage = favoritStorage;
        }

        public async Task<bool> Add(FavoritView favorit)
        {
            return await _favoritStorage.Add(favorit.ToDomain());
        }

        public async Task<IEnumerable<FavoritView>> ListAllFavorits()
        {
            IEnumerable<Favorit> favorits = await _favoritStorage.ListAllFavorits();
            return favorits.Select(f => new FavoritView(f));
        }

        public async Task<bool> Delete(FavoritView favorit)
        {
            return await _favoritStorage.Delete(favorit.ToDomain());
        } 
    }
}
