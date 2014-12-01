using System.Collections.Generic;
using System.Globalization;
using DarumaBLLPortable.ApplicationServices.Entites;
using DarumaBLLPortable.Common;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;
using DarumaDAL.WP.Abstraction;
using System.Threading.Tasks;
using DarumaDAL.WP.EntityMappers;

namespace DarumaDAL.WP.Storages
{
    public class FavoritStorage : StorageBase<Favorit, FavoritDTO>, IFavoritStorage
    {
        protected override string FolderName { get { return "Favorits"; } }

        private readonly IMapper<Favorit, FavoritDTO> _mapper = new FavoritMapper(); 

        protected override IMapper<Favorit, FavoritDTO> Mapper
        {
            get
            {
                return _mapper;
            }
        }

        private string GetFavoritId(Favorit favorit)
        {
            return favorit.Text.GetHashCode().ToString(CultureInfo.InvariantCulture);
        }

        public async Task<bool> Add(Favorit favorit)
        {
            string id = GetFavoritId(favorit);
            return await Add(favorit, id);
        }

        public async Task<IEnumerable<Favorit>> ListAllFavorits()
        {
            return await ListAll();
        }

        public async Task<bool> Delete(Favorit favorit)
        {
            return await Delete(GetFavoritId(favorit));
        }
    }
}
