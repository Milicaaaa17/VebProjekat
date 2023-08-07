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

        public async Task AzurirajKorisnika(KorisnikDTO korisnikDto)
        {
            bool dostupno = await _korisnikRepository.ProvjeriDostupnostKorisnickogImena(korisnikDto.KorisnickoIme);
            if (!dostupno)
            {
                throw new Exception("Korisnik sa datim korisničkim imenom već postoji.");
            }

            bool zauzet = await _korisnikRepository.ProvjeriZauzetostEmail(korisnikDto.Email);
            if (!zauzet)
            {
               throw new Exception("Korisnik sa datom email adresom već postoji.");
            }

            if (korisnikDto.Lozinka.Length < 8)
            {
                throw new Exception("Lozinka mora imati barem 8 karaktera.");
            }
            if (korisnikDto.Lozinka != korisnikDto.PonoviLozinku)
            {
                throw new Exception("Lozinke se ne poklapaju.");
            }
            Korisnik korisnik = _mapper.Map<Korisnik>(korisnikDto);
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

        public async Task<IEnumerable<Korisnik>> SviKorisnici()
        {
            return await _korisnikRepository.SviKorisnici();
        }
    }
}
