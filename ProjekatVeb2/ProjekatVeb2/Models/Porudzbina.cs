using System.ComponentModel.DataAnnotations.Schema;

namespace ProjekatVeb2.Models
{
    public class Porudzbina
    {
        public int Id { get; set; }
       
        public List<PoruzdbinaArtikal> PoruceniArtikli { get; set; }
      
        public string Adresa { get; set; }
        public string Komentar { get; set; }
        public double Cijena { get; set; }
        public Verifikacija VerifikacijaPorudzbine { get; set; }
        public DateTime VrijemePorucivanja { get; set; }
        public DateTime VrijemeDostave { get; set; }
        public Korisnik Korisnik { get; set; }
        public int IdKorisnik { get; set; }
        public double CijenaDostave { get; } = 300;
    }
}
