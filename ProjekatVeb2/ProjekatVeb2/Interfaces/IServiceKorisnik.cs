using ProjekatVeb2.DTO;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces
{
    public interface IServiceKorisnik
    {
        
            Task<List<KorisnikDTO>> GetSviKorisnici();
            Task<List<KorisnikDTO>> GetSviProdavci();

            Task<KorisnikDTO> GetById(int id);
            Task<KorisnikDTO> UpdateKorisnik(int id, UpdateKorisnikaDTO korisnikDTO);
            Task<KorisnikDTO> Verifikuj(int id);
            Task<KorisnikDTO> OdbijVerifikaciju(int id);
            Task<KorisnikDTO> Registracija(RegistracijaDTO registracijaDTO);
        
    }
}
