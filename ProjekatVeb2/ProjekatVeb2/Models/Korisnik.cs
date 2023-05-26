using System.ComponentModel.DataAnnotations;

namespace ProjekatVeb2.Models
{
    public class Korisnik
    {
       
        public int IdK { get; set; }
        public string KorisnickoIme { get; set; } 
    
        public string Email { get; set; }
      
        public string Lozinka { get; set; }
      
        public string ImePrezime { get; set; }

        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public TipKorisnika Tip { get; set; }
        public Verifikacija VerifikacijaKorisnika { get; set; }
        public byte[] Slika { get; set; }
        public List<Artikal> Artikili { get; set; }
        public List<Porudzbina> Porudzbine { get; set; }
        
        


    }
}
