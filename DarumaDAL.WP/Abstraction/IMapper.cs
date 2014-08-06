namespace DarumaDAL.WP.Abstraction
{
    public interface IMapper<Domian, DTO> 
    {
        Domian MapToDomain(DTO dto);
        DTO MapToDTO(Domian domain);
    }
}