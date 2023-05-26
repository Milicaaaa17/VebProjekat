using ProjekatVeb2.Models;

namespace ProjekatVeb2.DTO
{
    public class RegistracijaDTO
    {
      
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string PotvrdiLozinku { get; set; }
        public string ImePrezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public string Slika { get; set; }
        public TipKorisnika Tip { get; set; }
    }
}
