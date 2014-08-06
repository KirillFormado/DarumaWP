using DarumaBLLPortable.ApplicationServices.Entites;
using DarumaBLLPortable.Common;
using DarumaBLLPortable.Domain;
using DarumaDAL.WP.Abstraction;

namespace DarumaDAL.WP.EntityMappers
{
    public class DarumaMapper : IMapper<DarumaDomain, DarumaDTO>
    {
        public DarumaDTO MapToDTO(DarumaDomain domain)
        {
            return new DarumaDTO
            {
                Id = domain.Id,
                Wish = domain.Wish,
                Status = domain.Status,
                Theme = domain.Theme,
                CreateDate = domain.CreateDate,
                CurrentQuoteKey = domain.CurrentQuoteKey
            };
        }

        public DarumaDomain MapToDomain(DarumaDTO dto)
        {
            return DarumaDomainBuilder.BuildDaruma(dto);
        }
    }
}
