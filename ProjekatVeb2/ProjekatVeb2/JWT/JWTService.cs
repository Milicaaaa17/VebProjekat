using Microsoft.IdentityModel.Tokens;
using ProjekatVeb2.Data;
using ProjekatVeb2.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjekatVeb2.JWT
{
    public class JwtService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ContextDB _contextDB;
        private readonly IConfiguration _configuration;

        public JwtService(JwtSettings jwtSettings, ContextDB contextDB, IConfiguration configuration)
        {
            _jwtSettings = jwtSettings;
            _contextDB = contextDB;
            _configuration = configuration;
        }

        public string MakeAToken(LoginDTO loginDto)
        {
            var korisnik = _contextDB.Korisnici.FirstOrDefault(k => k.Email == loginDto.Email);


            byte[] keyBytes = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
            int requiredKeySize = 256 / 8; // 256 bits
            if (keyBytes.Length < requiredKeySize)
            {
                throw new Exception($"Key size must be greater than {requiredKeySize * 8} bits.");
            }

            var signingKey = new SymmetricSecurityKey(keyBytes);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

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


            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }


    }
}
