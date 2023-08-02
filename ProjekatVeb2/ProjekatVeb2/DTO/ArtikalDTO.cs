namespace ProjekatVeb2.DTO
{
    public class ArtikalDTO
    {
        public int IdArtikla { get; set; }
        public string Naziv { get; set; }
        public int Cijena { get; set; }
        public int Kolicina { get; set; }
        public string Opis { get; set; }
        public byte[]? Slika { get; set; }

    }
}
