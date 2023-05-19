namespace ProjekatVeb2.Models
{
    public class Administrator : Korisnik
    {
       public List<Kupac> Kupci { get; set; }
       public List<Prodavac> Prodavci { get; set; }
    }
}
