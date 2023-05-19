namespace ProjekatVeb2.Models
{
    public class Artikal
    {
        public string Id { get; set; }
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public int Kolicina { get; set; }
        public string Opis { get; set; }
        public byte[] Slika { get; set; }
        public Prodavac Prodavac { get; set; }
        public string ProdavacID { get; set; }

        public List<PoruzdbinaArtikal> PoruceniArtikli { get; set; }
       
    }
}
