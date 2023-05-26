namespace ProjekatVeb2.Models
{
    public class Artikal
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public int Kolicina { get; set; }
        public string Opis { get; set; }
        public byte[] Slika { get; set; }
        public Korisnik Korisnik { get; set; }
        public int IdKorisnik { get; set; }

        public List<PoruzdbinaArtikal> PoruceniArtikli { get; set; }
        public bool Izbrisan { get; set; }
       
    }
}
