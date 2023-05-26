using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces
{
    
        public interface IKorisnikRepository
        {
        Task<List<Korisnik>> GetSviKorisnici();
        Task<List<Korisnik>> GetSviProdavci();

        Task<Korisnik> GetById(int id);
        Task<Korisnik> UpdateKorisnik(Korisnik korisnik);
        Task<Korisnik> Verifikuj(int id);
        Task<Korisnik> OdbijVerifikaciju(int id);
        Task<Korisnik> Registracija(Korisnik korisnik);
        }
    
}
