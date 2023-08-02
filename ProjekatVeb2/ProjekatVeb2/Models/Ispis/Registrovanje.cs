using ProjekatVeb2.DTO;

namespace ProjekatVeb2.Models.Ispis
{
    public class Registrovanje
    {
        public bool KorisnikRegistrovan { get; set; }
        public List<string> Poruka { get; set; }
        public KorisnikDTO KorisnikDto { get; set; }
    }
}
