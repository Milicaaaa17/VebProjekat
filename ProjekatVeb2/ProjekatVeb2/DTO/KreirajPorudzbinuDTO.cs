namespace ProjekatVeb2.DTO
{
    public class KreirajPorudzbinuDTO
    {
        public string Komentar { get; set; }
        public string AdresaDostave { get; set; }
        public int Kolicina { get; set; }
        public List<StavkaDTO> Stavke { get; set; } = null;
        public DateTime VrijemeDostave { get; set; }
    }
}
