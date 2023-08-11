using com.sun.xml.@internal.ws.api;
using ProjekatVeb2.Data;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.JWT;
using ProjekatVeb2.Models.Ispis;

namespace ProjekatVeb2.Services
{
    public class LoginService : ILoginService
    {
        private readonly IKorisnikRepository _korisnikRepository;
        private readonly IEnkripcijaService _enkripcijaService;
        private readonly ContextDB _contextDB;
        private readonly JwtService _jwtService;
        private readonly IConfiguration _configuration;

        public LoginService(IKorisnikRepository korisnikRepository, IEnkripcijaService enkripcijaService, ContextDB contextDB, JwtService jwtService, IConfiguration configuration)
        {
            _korisnikRepository = korisnikRepository;
            _enkripcijaService = enkripcijaService;
            _contextDB = contextDB;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        public async Task<Prijava> PrijavaKorisnika(LoginDTO loginDto)
        {

            var hesLozinka = _enkripcijaService.EnkriptujLozinku(loginDto.Lozinka);
            bool ispravnoKor = await _korisnikRepository.ProvjeriDostupnostKorisnickogImena(loginDto.KorisnickoIme);
            if (!ispravnoKor)
            {
                return new Prijava { UspjesnaPrijava = false, Poruka = "Korisnicko ime ne postoji" };
            }

            bool ispravanEmail = await _korisnikRepository.ProvjeriIspravnostEmail(loginDto.Email);
            if (!ispravanEmail)
            {
                return new Prijava { UspjesnaPrijava = false, Poruka = "Email ne  postoji." };
            }

            bool ispravnaLozinka = await _korisnikRepository.ProvjeriIspravnostLozinke(hesLozinka);
            if (!ispravnaLozinka)
            {
                return new Prijava { UspjesnaPrijava = false, Poruka = "Netacna lozinka." };
            }


            string token = _jwtService.MakeAToken(loginDto);

            return new Prijava { UspjesnaPrijava = true, Poruka = "Uspjesna prijava",Token = token };

        }
    }
}
