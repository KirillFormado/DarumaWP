using DarumaBLLPortable.ApplicationServices.Abstractions;
using DarumaBLLPortable.ApplicationServices.Entites;
using DarumaBLLPortable.Common.Abstractions;
using DarumaBLLPortable.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DarumaBLLPortable.ViewModels
{
    public class DarumaShakeViewModel : ViewModelBase
    {
        private DarumaView _daruma;
        private IDarumaApplicationService _darumaService;

        public DarumaShakeViewModel(IDarumaApplicationService darumaService)
        {
            _darumaService = darumaService;
        }

        public async Task GetDarumaById(Guid id)
        {
            DarumaView daruma = await _darumaService.GetById(id);
            Daruma = daruma;
        }

        public async Task ChangeToTimeExpired()
        {
            _daruma = await _darumaService.ChangeStatus(_daruma, DarumaStatus.TimeExpired);
        }

        public async Task<bool> Delete(DarumaView daruma)
        {
            return await _darumaService.DeleteDaruma(daruma);
        }

        public string GetQuote(DarumaWishTheme theme, string key = null)
        {
            return _darumaService.GetQuote(theme, key);
        }

        public async Task<string> UpdateDarumaQuotation()
        {
            return await _darumaService.UpdateDarumaQuotation(Daruma);
        }

        public async Task TryExecuteWish(Func<bool> isExecuteWish)
        {
            if(Daruma.Status == DarumaStatus.MakedWish && isExecuteWish())
            {
                Daruma = await _darumaService.TryExecuteWish(Daruma);
            }
        }

        public DarumaView Daruma
        {
            get
            {
                return _daruma;
            }
            set
            {
                _daruma = value;
                OnPropertyChanged();
            }
        }


    }
}
