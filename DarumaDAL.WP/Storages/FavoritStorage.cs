using DarumaBLLPortable.Domain;
using DarumaDAL.WP.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarumaDAL.WP.Storages
{
    public class FavoritStorage : StorageBase<IEnumerable<Favorit>>
    {
        protected override string FolderName { get { return "Favorit"; } }
    }
}
