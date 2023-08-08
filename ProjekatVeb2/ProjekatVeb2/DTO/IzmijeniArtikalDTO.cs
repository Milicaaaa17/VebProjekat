namespace ProjekatVeb2.DTO
{
    public class IzmijeniArtikalDTO
    {
        public int IdArtikla { get; set; }
        public string Naziv { get; set; }
        public int Cijena { get; set; }
        public int Kolicina { get; set; }
        public string Opis { get; set; }
        public IFormFile Slika { get; set; }

    }
}
