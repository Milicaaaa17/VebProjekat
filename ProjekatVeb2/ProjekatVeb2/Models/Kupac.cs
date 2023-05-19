namespace ProjekatVeb2.Models
{
    public class Kupac : Korisnik
    {
       public List<Porudzbina> Porudzbina { get; set; }
       public Administrator Administrator { get; set; }
       public StatusPorudzbine StatusPorudzbine { get; set; }


    }
}
