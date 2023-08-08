using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces.IRepoistory
{
    public interface IPorudzbinaRepository
    {
        Task<List<Porudzbina>> SvePorudzbine();
        Task<Porudzbina> PorudzbinaNaOsnovuId(int id);
        Task DodajPorudzbinu(Porudzbina porudzbina);
        Task AzurirajPorudzbinu(Porudzbina porudzbina);
        Task ObrisiPorudzbinu(int id);
        Task<bool> PorudzbinaPostoji(int id);
       
    }
}
