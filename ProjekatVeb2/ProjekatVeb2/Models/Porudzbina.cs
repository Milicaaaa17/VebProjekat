using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjekatVeb2.Models
{
    public class Porudzbina
    {
        public int IdPorudzbine { get; set; }
        public int UkupnaCijena { get; set; }
        public string Komentar { get; set; }
        public string AdresaDostave { get; set; }
        public DateTime DatumPorudzbine { get; set; }
        public DateTime VrijemeDostave { get; set; }

        [JsonIgnore]
        public List<PoruzdbinaArtikal> PorudzbinaArtikal { get; set; }
        public Korisnik Korisnik { get; set; }
        public int KorisnikId { get; set; }
        public StatusPorudzbine Status { get; set; }
        public int CijenaDostave { get; set; }


        public Porudzbina()
        {
            PorudzbinaArtikal = new List<PoruzdbinaArtikal>();
        }
    }
}
