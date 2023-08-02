using ProjekatVeb2.DTO;
using ProjekatVeb2.Models.Ispis;

namespace ProjekatVeb2.Interfaces.IServices
{
    public interface IRegistracijaService
    {
        Task<Registrovanje> RegistrujKorisnika(RegistracijaDTO registracijaDto);
    }
}
