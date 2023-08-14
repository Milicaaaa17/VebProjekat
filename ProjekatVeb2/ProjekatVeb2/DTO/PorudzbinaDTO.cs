namespace ProjekatVeb2.DTO
{
    public class PorudzbinaDTO
    {

        public int IdPorudzbine { get; set; }
        public string Komentar { get; set; }
        public string AdresaDostave { get; set; }
        public List<PorudzbinaArtikalDto> PorudzbinaArtikalDto { get; set; }
    }
}
