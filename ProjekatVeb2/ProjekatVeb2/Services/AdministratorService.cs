using ProjekatVeb2.Data;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IKorisnikRepository _korisnikRepository;
        private readonly ContextDB _contextDB;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AdministratorService(IKorisnikRepository korisnikRepository, ContextDB contextDB, IConfiguration configuration, IEmailService emailService)
        {
            _korisnikRepository = korisnikRepository;
            _contextDB = contextDB;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<bool> AdminOdbijaVerifikacijuProdavca(int korisnikId)
        {

            var uspjesno = await _korisnikRepository.AzurirajStatusVerifikacije(korisnikId, false, StatusVerifikacije.Odbijen);
            if (uspjesno)
            {
                var korisnik = await _korisnikRepository.KorisnikNaOsnovuId(korisnikId);
                if (korisnik != null)
                {
                    var message = new Message
                    {
                        To = new List<string> { korisnik.Email },
                        Subject = "Verifikacija prodavca",
                        Content = "Vaš zahtjev za verifikaciju je odbijen."
                    };

                    _emailService.PosaljiEmail(message);
                }
            }

            return uspjesno;
        }

        public async Task<bool> AdminVerifikujeProdavca(int korisnikId)
        {


            var korisnik = await _korisnikRepository.KorisnikNaOsnovuId(korisnikId);
            if (korisnik == null)
            {
                return false;
            }

            var uspjesno = await _korisnikRepository.AzurirajStatusVerifikacije(korisnikId, true, StatusVerifikacije.Odobren);
            if (uspjesno)
            {
                var message = new Message
                {
                    To = new List<string> { korisnik.Email },
                    Subject = "Verifikacija prodavca",
                    Content = "Čestitamo! Vaš zahtjev za verifikaciju je prihvaćen."
                };

                _emailService.PosaljiEmail(message);
            }

            return uspjesno;
        }

        public async Task<IEnumerable<Korisnik>> SviProdavciKojiCekajuVerifikaciju()
        {
            return await _korisnikRepository.SviProdavciKojiCekajuVerifikaciju();
        }

        public async Task<IEnumerable<Korisnik>> SviProdavci()
        {
            return await _korisnikRepository.SviProdavci();
        }

        public async Task<Korisnik> VerifikovanProdavac(int id)
        {
            return await _korisnikRepository.KorisnikNaOsnovuId(id);
        }

        public async Task<Korisnik> ProdavacNaOsnovuId(int id)
        {
            return await _korisnikRepository.ProdavacNaOsnovuId(id);
        }

        public async Task<IEnumerable<Korisnik>> SviVerifikovaniProdavci()
        {
            return await _korisnikRepository.SviVerifikovaniProdavci();
        }

        public async Task<IEnumerable<Korisnik>> SviKorisnici()
        {
            return await _korisnikRepository.SviKorisnici();
        }

        public async Task<IEnumerable<Korisnik>> SviOdbijeniProdavci()
        {
            return await _korisnikRepository.SviOdbijeniProdavci();
        }
    }
}
