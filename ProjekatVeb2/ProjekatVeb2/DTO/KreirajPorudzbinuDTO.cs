namespace ProjekatVeb2.DTO
{
    public class KreirajPorudzbinuDTO
    {
        public string Komentar { get; set; }
        public string AdresaDostave { get; set; }
        public List<KreirajPorudzbinaArtikalDto> Stavke { get; set; } = null;
    }
}
