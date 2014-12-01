using System.Collections.Generic;
using System.Threading.Tasks;
using DarumaBLLPortable.ApplicationServices.Entites;

namespace DarumaBLLPortable.ApplicationServices.Abstractions
{
    public interface IFavoritApplicationService
    {
        Task<bool> Add(FavoritView favorit);
        Task<IEnumerable<FavoritView>> ListAllFavorits();
        Task<bool> Delete(FavoritView favorit);
    }
}