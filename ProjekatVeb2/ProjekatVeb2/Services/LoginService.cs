using com.sun.xml.@internal.ws.api;
using Google.Apis.Auth;
using Microsoft.IdentityModel.Tokens;
using ProjekatVeb2.Data;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces.IRepoistory;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.JWT;
using ProjekatVeb2.Models;
using ProjekatVeb2.Models.Ispis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ProjekatVeb2.Services
{
    public class LoginService : ILoginService
    {
        private readonly IKorisnikRepository _korisnikRepository;
        private readonly IEnkripcijaService _enkripcijaService;
        private readonly ContextDB _contextDB;
        private readonly JwtService _jwtService;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _section;

        public LoginService(IKorisnikRepository korisnikRepository, IEnkripcijaService enkripcijaService, ContextDB contextDB, JwtService jwtService, IConfiguration configuration, IConfigurationSection section)
        {
            _korisnikRepository = korisnikRepository;
            _enkripcijaService = enkripcijaService;
            _contextDB = contextDB;
            _jwtService = jwtService;
            _configuration = configuration;
            _section = section;
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

        public async Task<Prijava> LoginGoogle(string t)
        {
            var googleLoginDto = await ValidacijaTokena(t);
            if (googleLoginDto == null)
            {
                return new Prijava { UspjesnaPrijava = false, Poruka = "NEUSPJESNO" };
            }

            var korisnik = await _korisnikRepository.KorisnikNaOsnovuEmail(googleLoginDto.Email);


            if (korisnik == null)
            {

                if (korisnik == null)
                {
                    korisnik = new Korisnik()
                    {

                        Ime = googleLoginDto.Ime,
                        Prezime = googleLoginDto.Prezime,
                        KorisnickoIme = googleLoginDto.KorisnickoIme,
                        Email = googleLoginDto.Email,
                        Lozinka = "",
                        Adresa = "",
                        DatumRodjenja = DateTime.Now,
                        Tip = TipKorisnika.Kupac,
                        StatusVerifikacije = StatusVerifikacije.Odobren,
                        Slika = new byte[0],
                    };


                    _contextDB.Korisnici.Add(korisnik);
                    _contextDB.SaveChanges();
                }
            }


            // Generisanje ključa za potpisivanje tokena
            byte[] keyBytes = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
            int requiredKeySize = 256 / 8; // 256 bits
            if (keyBytes.Length < requiredKeySize)
            {
                throw new Exception($"Key size must be greater than {requiredKeySize * 8} bits.");
            }

            var signingKey = new SymmetricSecurityKey(keyBytes);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // Definisanje tvrdnji (claims) za token
            var claims = new[]
            {
                new Claim("Id", korisnik.Id.ToString()),
                new Claim("StatusVerifikacije", korisnik.StatusVerifikacije.ToString()),
                //new Claim("Lozinka", korisnik.Lozinka),
                new Claim(ClaimTypes.Role, korisnik.Tip.ToString())
            };


            // Kreiranje JWT tokena
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signingCredentials
            );

            // Generisanje string reprezentacije tokena
            var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
            return new Prijava { UspjesnaPrijava = true, Poruka = "Uspjesna prijava", Token = tokenHandler };

        }

        private async Task<GoogleLoginDto> ValidacijaTokena(string tok)
        {

            var validiran = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _section.Value }
            };

            var googleInfoKorisnika = await GoogleJsonWebSignature.ValidateAsync(tok, validiran);

            GoogleLoginDto googleLoginDto = new()
            {
                Email = googleInfoKorisnika.Email,
                KorisnickoIme = googleInfoKorisnika.Email.Split("@")[0],
                Ime = googleInfoKorisnika.GivenName,
                Prezime = googleInfoKorisnika.FamilyName
            };

            return googleLoginDto;
        }
    }
}
