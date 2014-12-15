using DarumaBLLPortable.Common;
using DarumaBLLPortable.Domain;
using DarumaDAL.WP.Abstraction;

namespace DarumaDAL.WP.EntityMappers
{
    public class FavoritMapper : IMapper<Favorit, FavoritDTO>
    {
        public FavoritDTO MapToDTO(Favorit favorit)
        {
            return new FavoritDTO
            {
                Id = favorit.Id,
                Text = favorit.Text,
                Theme = favorit.Theme
            };
        }

        public Favorit MapToDomain(FavoritDTO dto)
        {
            return new Favorit
            {
                Id = dto.Id,
                Text = dto.Text,
                Theme = dto.Theme
            };
        }
    }
}
