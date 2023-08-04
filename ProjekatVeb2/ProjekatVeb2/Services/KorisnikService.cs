using AutoMapper;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;

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
