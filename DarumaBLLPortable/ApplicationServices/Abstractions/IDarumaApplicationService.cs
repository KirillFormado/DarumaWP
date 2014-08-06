using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DarumaBLLPortable.ApplicationServices.Entites;
using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.ApplicationServices.Abstractions
{
    public interface IDarumaApplicationService
    {
        Task<DarumaView> GetById(Guid id);
        Task<IEnumerable<DarumaView>> CheckExpiredStatus(IEnumerable<DarumaView> darumaList);
        Task<IEnumerable<DarumaView>> ListAll();
        Task<DarumaView> TryExecuteWish(DarumaView daruma);
        Task<DarumaView> CreateDaruma(string wish, DarumaWishTheme theme);
        Task<DarumaView> ChangeStatus(DarumaView daruma, DarumaStatus status);
        Task<bool> DeleteDaruma(DarumaView daruma);

        Task<string> UpdateDarumaQuotation(DarumaView daruma);
        string GetQuote(DarumaWishTheme theme, string key = null);
        string GetQuotationForMainTile();
        KeyValuePair<string, string> GetQuotationByDarumaTheme(DarumaWishTheme theme);
        Task<KeyValuePair<DarumaView, string>> GetInfoForSercondaryTile(Guid id);        
    }
}