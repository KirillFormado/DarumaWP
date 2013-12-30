using DarumaBLLPortable.Domain;

namespace DarumaDAL.WP.Abstraction
{
    public interface IQuotationSource
    {
        string GetCitationSourse(DarumaWishTheme theme);
    }
}