using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces.IServices
{
    public interface IAdministratorService
    {
        Task<IEnumerable<Korisnik>> DohvatiRegistracijeZaOdobrenje();
        Task<bool> AdminOdobravaRegistraciju(int id);
        Task<bool> AdminOdbijaRegistraciju(int id);

        

        Task<IEnumerable<Korisnik>> SviProdavciKojiCekajuVerifikaciju();
        Task VerifikujProdavca(int prodavacId);
        Task OdbijVerifikacijuProdavca(int prodavacId);
    }
}
