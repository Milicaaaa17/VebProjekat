using Newtonsoft.Json;

namespace ProjekatVeb2.Models
{
    public class Artikal
    {
        public int IdArtikla { get; set; }
        public string Naziv { get; set; }
        public int Cijena { get; set; }
        public int Kolicina { get; set; }
        public string Opis { get; set; }
        public byte[]? Slika { get; set; }
        public Korisnik Korisnik { get; set; }
        public int KorisnikId { get; set; }

        [JsonIgnore]
        public List<PoruzdbinaArtikal> PoruceniArtikli { get; set; }
        
       
    }
}
