namespace ProjekatVeb2.DTO
{
    public class KreirajArtikalDTO
    {
        public string Naziv { get; set; }
        public int Cijena { get; set; }
        public int Kolicina { get; set; }
        public string Opis { get; set; }
        public IFormFile Slika { get; set; }
    }
}
