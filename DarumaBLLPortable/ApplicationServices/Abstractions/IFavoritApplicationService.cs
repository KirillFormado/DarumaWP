using System.Collections.Generic;
using System.Threading.Tasks;
using DarumaBLLPortable.ApplicationServices.Entites;

namespace DarumaBLLPortable.ApplicationServices.Abstractions
{
    public interface IFavoritApplicationService
    {
        Task<bool> Add(FavoritViewObj favorit);
        Task<IEnumerable<FavoritViewObj>> ListAllFavorits();
        Task<bool> Delete(FavoritViewObj favorit);
    }
}