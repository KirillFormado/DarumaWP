using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DarumaBLLPortable.ApplicationServices.Abstractions;
using DarumaBLLPortable.ApplicationServices.Entites;

namespace DarumaBLLPortable.ViewModels
{
    public class FavoritsViewModel
    {
        private readonly IFavoritApplicationService _service;
        private ObservableCollection<FavoritViewObj> _favorits = new ObservableCollection<FavoritViewObj>();

        public FavoritsViewModel(IFavoritApplicationService service)
        {
            _service = service;
        }

        public ObservableCollection<FavoritViewObj> Favorits
        {
            get { return _favorits; }
        }

        public async Task LoadFavorits()
        {
           // _favorits = new ObservableCollection<FavoritViewObj>();
            var favorits = await _service.ListAllFavorits();
            foreach (var favorit in favorits)
            {
                Favorits.Add(favorit);
            }
        }

        public async Task Delete(string id)
        {
            var guid = Guid.Parse(id);
            var quot = Favorits.FirstOrDefault(f => f.Id == guid);
            Favorits.Remove(quot);

            await _service.Delete(quot);
        }
    }
}
