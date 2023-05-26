using ProjekatVeb2.Models;

namespace ProjekatVeb2.DTO
{
    public class UpdateKorisnikaDTO
    {
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public string NovaLozinka { get; set; }
        public string StaraLozinka { get; set; }
        public string ImePrezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public string Slika { get; set; }
        
    }
}
