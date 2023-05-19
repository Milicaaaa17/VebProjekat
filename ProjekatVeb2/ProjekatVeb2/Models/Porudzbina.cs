using System.ComponentModel.DataAnnotations.Schema;

namespace ProjekatVeb2.Models
{
    public class Porudzbina
    {
        public int Id { get; set; }
       
        public Artikal Artikal { get; set; }
        public List<PoruzdbinaArtikal> PoruceniArtikli { get; set; }
        public string Kolicina { get; set; }
        public string AdresaDostave { get; set; }
        public DateTime VrijemeDostave { get; set; }
        public DateTime VrijemePorudzbine { get; set; }
        public string Komentar { get; set; }
        public float UkupnaCijena { get; set; }
        public float CijenaDostave { get; } = 200;       
        public StatusPorudzbine Status { get; set; }
        public Kupac Kupac { get; set; }
        public string KupacID { get; set; }
    }
}
