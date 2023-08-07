namespace ProjekatVeb2.DTO
{
    public class IzmjenaProfilaDTO
    {
        public int Id { get; set; }
        public string KorisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string PonovljenaLozinka { get; set; }
        public string Adresa { get; set; }
        public DateTime DatumRodjenja { get; set; }


        public IFormFile? Slika { get; set; }
    }
}
