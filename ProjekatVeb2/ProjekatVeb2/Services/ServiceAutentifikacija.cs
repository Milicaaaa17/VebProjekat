using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using ProjekatVeb2.DTO;
using ProjekatVeb2.Interfaces;
using ProjekatVeb2.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjekatVeb2.Services
{
    public class ServiceAutentifikacija : IServiceAutentifikacija
    {
        private readonly IKorisnikRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public ServiceAutentifikacija(IKorisnikRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<string> Login(LoginDTO loginDto)
        {
            var korisnici = await _repository.GetSviKorisnici();
            Korisnik korisnik = korisnici.FirstOrDefault(k => k.Email == loginDto.Email);
            if (korisnik == null)
                throw new Exception($"Korisnik sa emailom {loginDto.Email} ne postoji! Pokušajte ponovo.");
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Lozinka, korisnik.Lozinka))
                throw new Exception($"Netačna lozinka! Pokušajte ponovo.");

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ?? "default"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        new Claim("KorisnikId", korisnik.IdK.ToString()),
        new Claim("Email", korisnik.Email),
        new Claim(ClaimTypes.Role, korisnik.Tip.ToString()),
    };

            var key = new SymmetricSecurityKey(new byte[16]); // 16 bajta = 128 bita
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
