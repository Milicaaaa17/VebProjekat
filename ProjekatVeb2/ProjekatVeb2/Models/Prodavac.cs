namespace ProjekatVeb2.Models
{
    public class Prodavac : Korisnik
    {
        public List<Artikal> Artikli { get; set; }
        public List<Porudzbina> Poruzbine { get; set; }
        public Administrator Administrator { get; set; }
        public Verifikacija Verifikovan { get; set; }
        public StatusPorudzbine StatusPorudzbine { get; set; }
        


    }
}
