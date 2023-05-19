namespace ProjekatVeb2.Models
{
    public class PoruzdbinaArtikal
    {
        public Porudzbina Porudzbina { get; set; }
        public string IdPorudzbina { get; set; }
        public Artikal Artikal { get; set; }
        public string ArtikalID { get; set; }
        public int Kolicina { get; set; }
        public int Cijena { get; set; }

    }
}
