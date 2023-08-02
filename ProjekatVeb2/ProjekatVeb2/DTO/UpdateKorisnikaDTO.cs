using ProjekatVeb2.Models;

namespace ProjekatVeb2.DTO
{
    public class UpdateKorisnikaDTO
    {
        public int IdKorisnika { get; set; }
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string PonoviLozinku { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public TipKorisnika Tip { get; set; }
        public byte[] Slika { get; set; }

    }
}
