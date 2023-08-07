using ProjekatVeb2.DTO;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces.IServices
{
    public interface IKorisnikService
    {
        Task<Korisnik> KorisnikNaOsnovuId(int id);
        Task<IEnumerable<Korisnik>> SviKorisnici();
        Task AzurirajKorisnika(KorisnikDTO korisnikDto);

        Task<bool> BrisanjeKorisnikaNaOsnovuId(int id);


    }
}
