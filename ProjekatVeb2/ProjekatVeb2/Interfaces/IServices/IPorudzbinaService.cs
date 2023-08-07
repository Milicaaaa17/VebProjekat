using ProjekatVeb2.DTO;
using ProjekatVeb2.Models;

namespace ProjekatVeb2.Interfaces.IServices
{
    public interface IPorudzbinaService
    {
        Task<Porudzbina> PreuzmiPorudzbinuPoId(int id);
        Task<List<Porudzbina>> PreuzmiSvePorudzbine();
        Task DodajPorudzbinu(KreirajPorudzbinuDTO kreirajPorudzbinuDto);
        Task AzurirajPorudzbinu(PorudzbinaDTO porudzbinaDto);
        Task<bool> ObrisiPoruzbinu(int id);
    }
}
