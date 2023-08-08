

using Newtonsoft.Json;

namespace ProjekatVeb2.Models
{
    public class PoruzdbinaArtikal
    {
        [JsonIgnore]
        public Porudzbina Porudzbina { get; set; }
        public int IdPorudzbina { get; set; }
        [JsonIgnore]
        public Artikal Artikal { get; set; }
        public int ArtikalID { get; set; }
        public int Kolicina { get; set; }
       

    }
}
