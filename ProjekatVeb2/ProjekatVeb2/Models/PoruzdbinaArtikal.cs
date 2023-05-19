namespace ProjekatVeb2.Models
{
    public class PoruzdbinaArtikal
    {
        public Porudzbina Porudzbina { get; set; }
        public int PorudzbinaID { get; set; }
        public Artikal Artikal { get; set; }
        public int ArtikalID { get; set; }
        public int Kolicina { get; set; }
        public int Cijena { get; set; }

    }
}
