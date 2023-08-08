using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces.IRepoistory
{
    public interface IKorisnikRepository
    {
        //registarcija
        Task DodajKorisnika(Korisnik korisnik);
        Task<bool> ProvjeriDostupnostKorisnickogImena(string korisnickoIme); 
        Task<bool> ProvjeriZauzetostEmail(string email);

        //logovanje 
        Task<bool> ProvjeriIspravnostEmail(string email); 
        Task<bool> ProvjeriIspravnostLozinke(string lozinka);

        //
        Task<IEnumerable<Korisnik>> SviKorisnici(); 
        Task<List<Korisnik>> DobaviKorisnike();  
        Task<Korisnik> KorisnikNaOsnovuId(int id); 
        Task<Korisnik> KorisnikNaOsnovuEmail(string email); 
        Task AzurirajKorisnika(Korisnik korisnik); 
        Task BrisanjeKorisnikaNaOsnovuId(int id);

        //
        Task<IEnumerable<Korisnik>> SviProdavci();
        Task<Korisnik> ProdavacNaOsnovuId(int id);
        Task<IEnumerable<Korisnik>> SviVerifikovaniProdavci();
        Task<IEnumerable<Korisnik>> SviOdbijeniProdavci();
        Task<IEnumerable<Korisnik>> SviProdavciKojiCekajuVerifikaciju();
        Task<bool> AzurirajStatusVerifikacije(int korisnikId, bool verifikovan, StatusVerifikacije statusVerifikacije);


    }
}
