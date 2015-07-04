using System;
using System.Collections.Generic;
using DarumaBLLPortable.ApplicationServices.Abstractions;
using DarumaBLLPortable.Common.Abstractions;
using System.Threading.Tasks;
using System.Linq;
using DarumaBLLPortable.Domain;
using DarumaBLLPortable.ApplicationServices.Entites;

namespace DarumaBLLPortable.ApplicationServices.Services
{
    public class DarumaApplicationService : IDarumaApplicationService
    {
        private IDarumaStorage _storage;
        private IDarumaImageUriResolver _imageUriResolver;
        private IQuotationSource _quotationSource;

        public DarumaApplicationService(IDarumaStorage storage, IDarumaImageUriResolver imageUriResolver, IQuotationSource quotationSource)
        {
            _storage = storage;
            _imageUriResolver = imageUriResolver;
            _quotationSource = quotationSource;
        }

        public async Task<DarumaView> CreateDaruma(string wish, DarumaWishTheme theme)
        {
            var daruma = new DarumaDomain(wish, theme);
            //TODO: need another way handle exception
            bool result = await _storage.Add(daruma);
            if(!result)
            {
                throw new NullReferenceException();
            }
            return new DarumaView(daruma, _imageUriResolver);
        }

        public async Task<DarumaView> ChangeStatus(DarumaView daruma, DarumaStatus status)
        {
            DarumaDomain darumaDomain = await _storage.GetById(daruma.Id);
            darumaDomain.ChangeStatus(status);
            await _storage.Update(darumaDomain);

            return new DarumaView(darumaDomain, _imageUriResolver);
        }

        public async Task<bool> DeleteDaruma(DarumaView daruma)
        {           
            return await _storage.Delete(daruma.Id);            
        }

        public async Task<DarumaView> GetById(Guid id)
        {
            DarumaDomain daruma = await _storage.GetById(id);
            return new DarumaView(daruma, _imageUriResolver);
        } 

        public async Task<IEnumerable<DarumaView>> ListAll()
        {
            IEnumerable<DarumaDomain> all = await _storage.ListAll();
            return all.Where(d => d != null).Select(d => new DarumaView(d, _imageUriResolver));
        }
        
        public async Task<IEnumerable<DarumaView>> CheckExpiredStatus(IEnumerable<DarumaView> darumaList)
        {
            var darumas = await _storage.ListByIds(darumaList.Select(d => d.Id));

            foreach (var daruma in darumas)
            {
                if (daruma.IsExpired())
                {
                    daruma.ChangeStatus(DarumaStatus.TimeExpired);
                    await _storage.Update(daruma);
                }
            }

            return darumas.Select(d => new DarumaView(d, _imageUriResolver));
        }

        //TODO: may be need move to darumaDomain object
        public async Task<DarumaView> TryExecuteWish(DarumaView darumaView)
        {
            if (darumaView.Status != DarumaStatus.MakedWish)
                return darumaView;

            DarumaDomain daruma = await _storage.GetById(darumaView.Id);

            daruma.ChangeStatus(DarumaStatus.ExecutedWish);
            await _storage.Update(daruma);

            return new DarumaView(daruma, _imageUriResolver);
        }

        //public async Task UpdateQuoteOnTile(Guid id)
        //{
        //    DarumaDomain daruma = await _storage.GetById(id);
        //    var quoteSource = _quotationSource.GetQuotationSourse(daruma.Theme);
        //    daruma.CurrentQuoteKey = quoteSource.Key;
        //    await _storage.Update(daruma);
        //}

        //TODO: may be need quotation service
        public string GetQuote(DarumaWishTheme theme, string key = null)
        {
            var quote = key == null
                ? _quotationSource.GetQuotationSourse(theme).Value
                : _quotationSource.GetQuotationByKey(theme, key);
            return quote;
        }

        public string GetQuotationForMainTile()
        {
            var quoteSource = GetQuotationByDarumaTheme(DarumaWishTheme.NoSet);
            var quote = quoteSource.Value;
            return quote;
        }

        public KeyValuePair<string, string> GetQuotationByDarumaTheme(DarumaWishTheme theme)
        {
            var quoteSource = _quotationSource.GetQuotationSourse(theme);
            return quoteSource;
        }       

        public async Task<KeyValuePair<DarumaView, string>> GetInfoForSercondaryTile(Guid id)
        {
            DarumaDomain daruma = await _storage.GetById(id);
            var quoteSource = GetQuotationByDarumaTheme(daruma.Theme);
            var quote = quoteSource.Value;
            daruma.CurrentQuoteKey = quoteSource.Key;
            await _storage.Update(daruma);
            return new KeyValuePair<DarumaView, string>(new DarumaView(daruma, _imageUriResolver), quote);
        }

        public async Task<DarumaView> UpdateQuotation(Guid id, string quoteKey)
        {
            DarumaDomain darumaDomain = await _storage.GetById(id);
            darumaDomain.CurrentQuoteKey = quoteKey;
            await _storage.Update(darumaDomain);

            return new DarumaView(darumaDomain, _imageUriResolver);



        }

        public async Task<string> UpdateDarumaQuotation(DarumaView daruma)
        {
            var quotationSourse = GetQuotationByDarumaTheme(daruma.Theme);

            daruma.CurrentQuoteKey = quotationSourse.Key;
            await _storage.Update(DarumaDomainBuilder.BuildDaruma(daruma));

            return quotationSourse.Value;
        }       
    }
}
