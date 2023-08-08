using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces.IServices
{
    public interface IAdministratorService
    {

        Task<IEnumerable<Korisnik>> SviProdavci();
        Task<IEnumerable<Korisnik>> SviVerifikovaniProdavci();
        Task<IEnumerable<Korisnik>> SviOdbijeniProdavci();
        Task<Korisnik> ProdavacNaOsnovuId(int id);
        Task<IEnumerable<Korisnik>> SviKorisnici();
        Task<IEnumerable<Korisnik>> SviProdavciKojiCekajuVerifikaciju();
        Task<bool> AdminVerifikujeProdavca(int korisnikId);
        Task<bool> AdminOdbijaVerifikacijuProdavca(int korisnikId);
        Task<Korisnik> VerifikovanProdavac(int id);
    }
}
