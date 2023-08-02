using ProjekatVeb2.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjekatVeb2.DTO
{
    public class RegistracijaDTO
    {

       
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string PonoviLozinku { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public TipKorisnika Tip { get; set; }

        public IFormFile? Slika { get; set; }
    }
}
