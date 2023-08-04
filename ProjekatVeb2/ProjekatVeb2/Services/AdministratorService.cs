using ProjekatVeb2.Data;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IKorisnikRepository _korisnikRepository;

        public AdministratorService(IKorisnikRepository korisnikRepository)
        {
            _korisnikRepository = korisnikRepository;
        }

        public async Task<bool> AdminOdbijaRegistraciju(int id)
        {
            var korisnik = await _korisnikRepository.KorisnikNaOsnovuId(id);
            if (korisnik != null && !korisnik.Verifikovan)
            {
                await _korisnikRepository.BrisanjeKorisnikaNaOsnovuId(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AdminOdobravaRegistraciju(int id)
        {
            var korisnik = await _korisnikRepository.KorisnikNaOsnovuId(id);
            if (korisnik != null && !korisnik.Verifikovan)
            {
                korisnik.Verifikovan = true;
                await _korisnikRepository.AzurirajKorisnika(korisnik);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Korisnik>> DohvatiRegistracijeZaOdobrenje()
        {
            return await _korisnikRepository.KorisniciCekajuOdobrenje(false);
        }

        public Task OdbijVerifikacijuProdavca(int prodavacId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Korisnik>> SviProdavciKojiCekajuVerifikaciju()
        {
            throw new NotImplementedException();
        }

        public Task VerifikujProdavca(int prodavacId)
        {
            throw new NotImplementedException();
        }
    }
}
