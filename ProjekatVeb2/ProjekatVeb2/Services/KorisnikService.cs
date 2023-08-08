using AutoMapper;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;
using ProjekatVeb2.Models.Ispis;

namespace ProjekatVeb2.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly IKorisnikRepository _korisnikRepository;
        private readonly IMapper _mapper;
        private readonly IEnkripcijaService _enkripcijaService;

        public KorisnikService(IKorisnikRepository korisnikRepository, IMapper mapper, IEnkripcijaService enkripcijaService)
        {
            _korisnikRepository = korisnikRepository;
            _mapper = mapper;
            _enkripcijaService = enkripcijaService;
        }

        public async Task AzurirajKorisnika(IzmjenaProfilaDTO korisnikDto)
        {

            if (korisnikDto.Lozinka.Length < 8)
            {
                throw new Exception("Lozinka mora imati barem 8 karaktera.");
            }

            if (korisnikDto.Lozinka != korisnikDto.PonovljenaLozinka)
            {
                throw new Exception("Lozinke se ne poklapaju.");
            }

            // Dohvati korisnika iz baze podataka
            Korisnik postojeciKorisnik = await _korisnikRepository.KorisnikNaOsnovuId(korisnikDto.Id);
            if (postojeciKorisnik == null)
            {
                throw new Exception("Korisnik ne postoji.");

            }
            postojeciKorisnik.KorisnickoIme = korisnikDto.KorisnickoIme;
            postojeciKorisnik.Email = korisnikDto.Email;
            postojeciKorisnik.Ime = korisnikDto.Ime;
            postojeciKorisnik.Prezime = korisnikDto.Prezime;
            postojeciKorisnik.Lozinka = _enkripcijaService.EnkriptujLozinku(korisnikDto.Lozinka);
            postojeciKorisnik.PonoviLozinku = _enkripcijaService.EnkriptujLozinku(korisnikDto.PonovljenaLozinka);
            postojeciKorisnik.Adresa = korisnikDto.Adresa;
            postojeciKorisnik.DatumRodjenja = korisnikDto.DatumRodjenja;


            if (korisnikDto.Slika != null)
            {
                using (var ms = new MemoryStream())
                {
                    korisnikDto.Slika.CopyTo(ms);
                    var slikaByte = ms.ToArray();
                    postojeciKorisnik.Slika = slikaByte;

                }
            }


            if (korisnikDto.KorisnickoIme != postojeciKorisnik.KorisnickoIme)
            {
                throw new Exception("Vec postoji u bazi korisnik sa tim korisnickim imenom");
            }

            if (korisnikDto.Email != postojeciKorisnik.Email)
            {
                throw new Exception("Vec postoji u bazi korisnik sa tim email ");
            }

            await _korisnikRepository.AzurirajKorisnika(postojeciKorisnik);
        }

        public async Task<bool> BrisanjeKorisnikaNaOsnovuId(int id)
        {
            var korisnik = await _korisnikRepository.KorisnikNaOsnovuId(id);
            if (korisnik != null)
            {
                await _korisnikRepository.BrisanjeKorisnikaNaOsnovuId(id);
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<Korisnik> KorisnikNaOsnovuId(int id)
        {
            return await _korisnikRepository.KorisnikNaOsnovuId(id);
        }

    }
}
