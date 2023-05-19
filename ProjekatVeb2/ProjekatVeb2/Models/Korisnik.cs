using System.ComponentModel.DataAnnotations;

namespace ProjekatVeb2.Models
{
    public class Korisnik
    {
        public int Id { get; set; }
   
        public string KorisnickoIme { get; set; } //kljuc
    
        public string Email { get; set; }
      
        public string Lozinka { get; set; }
      
        public string ImePrezime { get; set; }

        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        
        public string Slika { get; set; }
        


    }
}
