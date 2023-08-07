using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces.IServices
{
    public interface IPorudzbinaService
    {
        Task<Porudzbina> PreuzmiPorudzbinuPoId(int id);
        Task<List<Porudzbina>> PreuzmiSvePorudzbine();
        Task DodajPorudzbinu(Porudzbina porudzbina);
        Task AzurirajPorudzbinu(Porudzbina porudzbina);
        Task<bool> ObrisiPoruzbinu(int id);
    }
}
