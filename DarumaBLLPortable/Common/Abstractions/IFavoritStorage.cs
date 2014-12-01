using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarumaBLLPortable.ApplicationServices.Entites;
using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.Common.Abstractions
{
    public interface IFavoritStorage
    {
        Task<bool> Add(Favorit favorit);
        Task<IEnumerable<Favorit>> ListAllFavorits();
        Task<bool> Delete(Favorit favorit);
    }
}
