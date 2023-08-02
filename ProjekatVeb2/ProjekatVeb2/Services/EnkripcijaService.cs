using ProjekatVeb2.Interfaces.IServices;
using System.Security.Cryptography;
using System.Text;

namespace ProjekatVeb2.Services
{
    public class EnkripcijaService : IEnkripcijaService
    {
        public string EnkriptujLozinku(string lozinka)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Konvertuj lozinku u niz bajtova
                byte[] lozinkaBytes = Encoding.UTF8.GetBytes(lozinka);

                // Izračunaj hash lozinke
                byte[] hashBytes = sha256.ComputeHash(lozinkaBytes);

                // Konvertuj hash u string
                string hashLozinke = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                return hashLozinke;
            }
        }
    }
}
