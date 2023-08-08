using ProjekatVeb2.DTO;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces.IServices
{
    public interface IKorisnikService
    {
        Task<Korisnik> KorisnikNaOsnovuId(int id);
        Task AzurirajKorisnika(IzmjenaProfilaDTO izmjenaProfilaDto);
        Task<bool> BrisanjeKorisnikaNaOsnovuId(int id);


    }
}
